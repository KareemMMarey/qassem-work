


$(document).ready(function () {
    
    const emptyGuid = '00000000-0000-0000-0000-000000000000';

    const storedServiceData = JSON.parse(localStorage.getItem(`serviceData_${serviceId}`)) || {};
    const requestId = JSON.parse(localStorage.getItem(`requestId_${serviceId}`));

    if (storedServiceData.currentStep) {
        currentStep = storedServiceData.currentStep;
        requestData = storedServiceData.requestData || {};
        attachments = storedServiceData.attachments || [];

        // Update the hidden requestId field if available
        if (requestId) {
            $("#requestId").val(requestId);
        }
    }
})
// Upload files
$(".attachment-input").on("change", function () {
    
    const attachmentId = $(this).data("attachment-id");
    const attachmentName = $(this).siblings("label").text().trim();
    const maxSizeMB = $(this).data("max-size");
    //const allowedExtensions = $(this).data("allowed-extensions").split(",");
    const files = $(this).prop("files");
    const formData = new FormData();

    const userId = "admin"; //"@ViewBag.UserId";
    const requestId = $("#requestId").val()//"a4cbe8f4-2b3e-4801-9703-93a6a3866e8f";//"@ViewBag.RequestId";

    // Add user and request IDs
    formData.append("userId", userId);
    formData.append("requestId", requestId);
    formData.append("attachmentTypeId", attachmentId != null && attachmentId > 0 ? attachmentId : 1);

    const errorMessages = [];

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const fileSizeMB = file.size / (1024 * 1024);
        const fileExtension = file.name.split('.').pop().toLowerCase();

        // Validate file size
        if (fileSizeMB > maxSizeMB) {
            const message = attachmentmessages.fileSizeExceeded.replace("{0}", file.name).replace("{1}", maxSizeMB);
            errorMessages.push(message);
            continue;
        }

        // Validate file extension
        //if (!allowedExtensions.includes(fileExtension)) {
        //    const message = attachmentmessages.unsupportedExtension.replace("{0}", file.name).replace("{1}", allowedExtensions.join(", "));
        //    errorMessages.push(message);
        //    continue;
        //}

        // Add the valid file to the form data
        formData.append("files", file);
        // Save the attachment with ID and Name

        const size = fileSizeMB * 1024;
        attachments.push({
            id: attachmentId != null && attachmentId > 0 ? attachmentId : 1,
            name: file.name,
            typeName: attachmentName,
            size: parseInt(size),
            extension: fileExtension,
            url: "/" + requestId + "/" + file.name

        });
    }


    if (errorMessages.length > 0) {
        showPopup("error", "Error", errorMessages.join("\n"));
        return;
    }

    // Save to service-specific storage
    const serviceData = {
        currentStep: currentStep,
        requestData: requestData,
        attachments: attachments
    };
    localStorage.setItem(`serviceData_${serviceId}`, JSON.stringify(serviceData));

    // Upload files
    $.ajax({
        url: "/Request/UploadAttachments",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            const uploadedList = $(`#uploaded-files-${attachmentId}`);
            response.forEach(file => {
                uploadedList.append(`<li>${file.fileName}</li>`);
            });
            showPopup("success", "Success", attachmentmessages.successUploadMessage);
        },
        error: function (xhr) {
            showPopup("error", "Error", attachmentmessages.uploadFailed)
        }
    });
});


function deleteAttachment(fileName) {
    const requestId = localStorage.getItem(`requestId_${serviceId}`);
    const serviceData = JSON.parse(localStorage.getItem(`serviceData_${serviceId}`)) || {};

    currentStep = serviceData.currentStep;
    requestData = serviceData.requestData || {};
    attachments = serviceData.attachments || [];

    attachments = attachments.filter(f => f.name !== fileName);


    const formData = new FormData();
    formData.append("requestId", requestId);
    formData.append("fileName", fileName);
    $.ajax({
        url: "/Request/DeleteAttachment",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {

            const serviceData = {
                currentStep: currentStep,
                requestData: requestData,
                attachments: attachments
            };
            localStorage.setItem(`serviceData_${serviceId}`, JSON.stringify(serviceData));
        },
        error: function (xhr) {
            showPopup("error", "Error", attachmentmessages.uploadFailed)
        }
    });

}





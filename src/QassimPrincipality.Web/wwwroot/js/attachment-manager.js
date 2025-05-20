
// Upload files
$(".attachment-input").on("change", function () {
    const attachmentId = $(this).data("attachment-id");
    const attachmentName = $(this).siblings("label").text().trim();
    const maxSizeMB = $(this).data("max-size");
    const allowedExtensions = $(this).data("allowed-extensions").split(",");
    const files = $(this).prop("files");
    const formData = new FormData();

    const userId = "admin"; //"@ViewBag.UserId";
    const requestId = "a4cbe8f4-2b3e-4801-9703-93a6a3866e8f";//"@ViewBag.RequestId";

    // Add user and request IDs
    formData.append("userId", userId);
    formData.append("requestId", requestId);
    formData.append("attachmentTypeId", attachmentId);

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
        if (!allowedExtensions.includes(fileExtension)) {
            const message = attachmentmessages.unsupportedExtension.replace("{0}", file.name).replace("{1}", allowedExtensions.join(", "));
            errorMessages.push(message);
            continue;
        }

        // Add the valid file to the form data
        formData.append("files", file);
        // Save the attachment with ID and Name
        attachments.push({
            id: attachmentId,
            name: file.name,
            typeName: attachmentName
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




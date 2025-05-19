let currentStep = parseInt(localStorage.getItem("currentStep")) || 1;
const totalSteps = @Model.ServiceSteps.Count;
const serviceId = @Model.Id;
$(document).ready(function () {
    // Initial Setup
    //const serviceId = @Model.Id;
    // const totalSteps = @Model.ServiceSteps.Count;
    const emptyGuid = '00000000-0000-0000-0000-000000000000';
    //let currentStep = parseInt(localStorage.getItem("currentStep")) || 1;
    let requestData = JSON.parse(localStorage.getItem("requestData")) || {};
    let attachments = JSON.parse(localStorage.getItem("attachments")) || [];

    // Load the current step on page load
    localStorage.setItem("currentStep", currentStep);
    loadStep(serviceId, currentStep);
    updateStepper(currentStep);
    updateButtonVisibility();

    // Next Button Click Event
    $("#next-btn").click(function () {


        // Handle first step details validation
        if (currentStep === 1) {
            const details = $("#requestDetails").val().trim();
            if (details === "") {
                showErrorMessage("#requestDetails", messages.detailsMessage);
                return;
            }
        }

        // Validate required attachments
        if (!validateRequiredAttachments()) {
            showPopup("error", messages.errorTitle, messages.requiredFilesMessage);
            return;
        }

        // Save current step data
        saveCurrentStepData(currentStep);

        // Move to the next step
        currentStep++;
        localStorage.setItem("currentStep", currentStep);
        loadStep(serviceId, currentStep);
        updateStepper(currentStep);
        updateButtonVisibility();
    });
    $("#previous-btn").click(function () {
        if (currentStep > 1) {
            currentStep--;
            localStorage.setItem("currentStep", currentStep);
            loadStep(serviceId, currentStep);
            updateStepper(currentStep);
            updateButtonVisibility();
        }
    });
    // Submit Button Click Event
    $("#submit-btn").click(function () {
        if ($("#step-form").valid()) {
            submitRequest();
        }
    });

    // Load Step Content via AJAX
    function loadStep(serviceId, stepNumber) {
        $("#loading-indicator").show();

        $.get(`/Request/LoadStep?serviceId=${serviceId}&stepNumber=${stepNumber}`)
            .done(function (data) {
                $("#step-content").html(data);
                $("#loading-indicator").hide();
                initializeFormValidation();

                // Check if this is the review step
                if (stepNumber === totalSteps) {
                    loadReviewData();
                } else {
                    // Restore saved data for the current step
                    const stepData = requestData[`Step${stepNumber}`];
                    if (stepData) {
                        for (const [key, value] of Object.entries(stepData)) {
                            $(`#${key}`).val(value);
                        }
                    }
                }

                // Restore saved data for the current step
                // const stepData = requestData[`Step${stepNumber}`];
                // if (stepData) {
                //     for (const [key, value] of Object.entries(stepData)) {
                //         $(`#${key}`).val(value);
                //     }
                // }

                // Handle file uploads if on the last step
                if (stepNumber === totalSteps - 1) {
                    handleFileUploads();
                }
            })
            .fail(function (xhr) {
                showError(messages.failedToLoadStep + ' ' + xhr.responseText);
            });
    }

    // Initialize Form Validation
    function initializeFormValidation() {
        $("#step-form").validate({
            errorClass: "text-danger",
            errorElement: "div",
            highlight: function (element) {
                $(element).closest(".form-group").addClass("has-error");
            },
            unhighlight: function (element) {
                $(element).closest(".form-group").removeClass("has-error");
            }
        });
    }

    // Save Data for the Current Step
    function saveCurrentStepData(stepNumber) {
        const customHiddenInput = $("#customrequesterRelation");
        if (customHiddenInput.length) {
            const relationValue = customHiddenInput.val();
            if (!relationValue) {
                showError(messages.pleaseSelectRelation );
                return false;
            }
            console.log("Submitting Requester Relation:", relationValue);
        }

        const stepContent = $("#step-content").find("input, textarea, select");
        let stepData = {};

        stepContent.each(function () {
            const id = $(this).attr("id");
            const value = $(this).val();

            // Check if we are on Step 1
            const currentStep = localStorage.getItem("currentStep");
            if (currentStep === "1" && id) {
                // Use the label text instead of the ID for Step 1
                const labelElement = $(`label[for="${id}"]`);
                const label = labelElement.length ? labelElement.text().trim() : id;
                stepData[label] = value;
            } else if (id) {
                // Use regular ID for other steps
                stepData[id] = value;
            }
        });

        // Store data locally
        requestData[`Step${stepNumber}`] = stepData;
        localStorage.setItem("requestData", JSON.stringify(requestData));
        console.log("Current Step Data:", requestData);

        // Check if the current step is "Attachments"
        const stepName = $("#step-title").data("step-name") || "";
        if (stepName.toLowerCase().includes("attachments")) {
            console.log("Skipping server save for attachments step");
            return;
        }

        // Save to server
        saveToServer(stepNumber, stepData);
    }

    // Save to Server
    function saveToServer(stepNumber, stepData) {
        const requestId = $("#requestId").val() || emptyGuid;

        $.post("/Request/SaveStepData", {
            requestId: requestId,
            serviceId: serviceId,
            stepNumber: stepNumber,
            stepData: JSON.stringify(stepData)
        }).fail(function (xhr) {
            "error", messages.errorTitle,
            showError(messages.failedToSaveStepData + ' ' + xhr.responseText);
        });
    }

    // Submit Full Request
    function submitRequest() {
        const requestDetails = JSON.stringify(requestData);
        const requestId = $("#requestId").val() || emptyGuid;

        $.post("/Request/SubmitRequest", {
            requestId: requestId,
            serviceId: serviceId,
            requestDetails: requestDetails,
            attachments: JSON.stringify(attachments)
        })
            .done(function (response) {
                if (response.success) {
                    showError(messages.requestSubmitted);
                    clearLocalStorage();
                    window.location.href = "/Request/Success";
                } else {
                    showPopup("error", messages.errorTitle, messages.failedToSubmitRequest);
                }
            })
            .fail(function (xhr) {
                showError(messages.failedToSubmitRequest + ' ' + xhr.responseText);
            });
    }

    // Handle File Uploads
    function handleFileUploads() {
        $("#attachments").on("change", function () {
            const files = $(this).prop("files");
            const formData = new FormData();

            for (let i = 0; i < files.length; i++) {
                formData.append("file", files[i]);
            }

            $.ajax({
                url: "/Request/UploadAttachment",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    attachments.push(response.fileName);
                    localStorage.setItem("attachments", JSON.stringify(attachments));
                    $("#uploaded-files").append(`<li>${response.fileName}</li>`);
                    console.log("Attachments:", attachments);
                },
                error: function (xhr) {
                    showError(messages.failedToUploadFile + ' ' + xhr.responseText);
                }
            });
        });
    }

    // Show Error Messages
    function showError(message) {
        $("#error-message").text(message).show();
    }

    // Show Inline Error Message
    function showErrorMessage(selector, message) {
        $(selector).next(".error").text(message).show();
    }

    // Hide Inline Error Message
    function hideErrorMessage(selector) {
        $(selector).next(".error").hide();
    }

    // Clear Local Storage on Completion
    function clearLocalStorage() {
        localStorage.removeItem("currentStep");
        localStorage.removeItem("requestData");
        localStorage.removeItem("attachments");
    }
    function updateStepper(currentStep) {
        debugger;
        // Remove active class from all steps
        $(".pc-step").removeClass("active");

        // Add active class to the current step and all previous steps
        $(".pc-step").each(function () {

            const stepNumber = parseInt($(this).find(".pc-step-circle").text().trim());
            if (stepNumber <= currentStep) {
                $(this).addClass("active");
            }
        });
    }
    // Validate Required Attachments
    function validateRequiredAttachments() {
        let allAttachmentsValid = true;

        $(".attachment-input.required-file").each(function () {
            const attachmentId = $(this).data("attachment-id");
            const uploadedFiles = $(`#uploaded-files-${attachmentId} li`);

            if (uploadedFiles.length === 0) {
                allAttachmentsValid = false;
            }
        });

        return allAttachmentsValid;
    }
    // Update Button Visibility
    function updateButtonVisibility() {
        if (currentStep === 1) {
            $("#previous-btn").hide();
            $("#next-btn").show();
            $("#submit-btn").hide();
        } else if (currentStep === totalSteps) {
            $("#previous-btn").show();
            $("#next-btn").hide();
            $("#submit-btn").show();
        } else {
            $("#previous-btn").show();
            $("#next-btn").show();
            $("#submit-btn").hide();
        }
    }

    function loadReviewData() {
        const requestData = JSON.parse(localStorage.getItem("requestData") || "{}");
        const reviewContainer = $("#review-data");
        const attachmentContainer = $("#review-attachments");

        // Clear previous content
        reviewContainer.empty();
        attachmentContainer.empty();

        // Iterate through each step
        for (const [stepKey, stepValues] of Object.entries(requestData)) {
            for (const [key, value] of Object.entries(stepValues)) {
                // Handle attachments separately
                if (key.startsWith("attachment-")) {
                    const fileName = value.split("\\").pop();
                    attachmentContainer.append(`
                            <li>
                                <i class="fas fa-file"></i> ${fileName}
                            </li>
                        `);
                } else {
                    // Format field names for better readability
                    const formattedKey = key
                        .replace(/([A-Z])/g, " $1")
                        .replace(/-/g, " ")
                        .replace(/_/g, " ")
                        .replace(/attachment /g, "")
                        .trim();

                    reviewContainer.append(`
                            <li>
                                <span class="review-label">${formattedKey}:</span>
                                <span class="review-value">${value}</span>
                            </li>
                        `);
                }
            }
        }
    }



});
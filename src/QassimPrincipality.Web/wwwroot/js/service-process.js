
$(document).ready(function () {
    // Initial Setup
    
    const emptyGuid = '00000000-0000-0000-0000-000000000000';

    const storedServiceData = JSON.parse(localStorage.getItem(`serviceData_${serviceId}`)) || {};
    if (storedServiceData.currentStep) {
        currentStep = storedServiceData.currentStep;
        requestData = storedServiceData.requestData || {};
        attachments = storedServiceData.attachments || [];

        // Update the hidden requestId field if available
        if (storedServiceData.requestId) {
            $("#requestId").val(storedServiceData.requestId);
            console.log("Loaded requestId from storage:", storedServiceData.requestId);
        }

        loadStep(serviceId, currentStep);
        updateStepper(currentStep);
        updateButtonVisibility();
    } else {
        // Start from the first step if no matching data found
        currentStep = 1;
        loadStep(serviceId, currentStep);
    }

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
        //localStorage.setItem("currentStep", currentStep);
        loadStep(serviceId, currentStep);
        updateStepper(currentStep);
        updateButtonVisibility();

         // Store the current step with service ID
        const serviceData = {
            currentStep: currentStep,
            requestData: requestData,
            attachments: attachments
        };
        localStorage.setItem(`serviceData_${serviceId}`, JSON.stringify(serviceData));
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
        //if ($("#step-form").valid()) {
            submitRequest();
        //}
    });

    // Load Step Content via AJAX
    function loadStep(serviceId, stepNumber) {
        $("#loading-indicator").show();

        $.get(`/Request/LoadStep?serviceId=${serviceId}&stepNumber=${stepNumber}`)
            .done(function (data) {
                $("#step-content").html(data);
                $("#loading-indicator").hide();
                initializeFormValidation();

                // Update total steps based on returned data
                // const stepsData = JSON.parse($("#total-steps-data").val() || "[]");
                // totalSteps = stepsData.length + 2; // Including Attachments and Review

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
        if (currentStep !== totalSteps - 1) {
            stepContent.each(function () {
                const id = $(this).attr("id");
                const value = $(this).val();
                if (id) {
                    stepData[id] = value;
                }
            });
        }
        // Store data locally with service ID
        requestData[`Step${stepNumber}`] = stepData;
        const serviceData = {
            currentStep: currentStep,
            requestData: requestData,
            attachments: attachments
        };
        localStorage.setItem(`serviceData_${serviceId}`, JSON.stringify(serviceData));
        console.log("Current Step Data:", requestData);

        // Check if the current step is "Attachments"

        if (currentStep === totalSteps-1) {
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
        }).done(function (response) {
            // Check if the response contains a new requestId
            if (response.requestId && response.requestId !== emptyGuid) {
                // Update the hidden input
                $("#requestId").val(response.requestId);

                // Update local storage with the new request ID
                const serviceData = JSON.parse(localStorage.getItem(`serviceData_${serviceId}`)) || {};
                serviceData.requestId = response.requestId;
                localStorage.setItem(`serviceData_${serviceId}`, JSON.stringify(serviceData));

                console.log("Request ID updated:", response.requestId);
            }
        }).fail(function (xhr) {
            "error", messages.errorTitle,
            showError(messages.failedToSaveStepData + ' ' + xhr.responseText);
        });
        $("#requestId").val('@ViewBag.RequestId')
    }

    // Submit Full Request
    function submitRequest() {
        //const requestDetails = JSON.stringify(requestData);
        const requestId = $("#requestId").val() || emptyGuid;

        $.post("/Request/SubmitRequest", {
            requestId: requestId
        })
            .done(function (response) {
                if (response.success) {
                    const requestNumber = response.requestNumber || "UNKNOWN";
                    clearServiceData(serviceId);
                    window.location.href = `/Request/Success?requestNumber=${requestNumber}`;
                } else {
                    showPopup("error", messages.errorTitle, messages.failedToSubmitRequest);
                }
            })
            .fail(function (xhr) {
                showError(messages.failedToSubmitRequest + ' ' + xhr.responseText);
            });
    }

    // Handle File Uploads
    

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
        const serviceData = JSON.parse(localStorage.getItem(`serviceData_${serviceId}`) || "{}");
        const requestData = serviceData.requestData || {};
        const attachments = serviceData.attachments || [];

        const reviewContainer = $("#review-data");
        const attachmentContainer = $("#review-attachments");
        const additionalDataContainer = $("#review-additionaldata");

        // Clear previous content
        reviewContainer.empty();
        additionalDataContainer.empty();
        attachmentContainer.empty();
        // Track if Step 2 exists
        let hasStep2Data = false;
        // Iterate through each step
        let hasAttachments = attachments.length > 0;
        Object.entries(requestData).forEach(([stepKey, stepValues]) => {
            // Step 1 (Basic Data)
            if (stepKey === "Step1") {
                Object.entries(stepValues).forEach(([key, value]) => {
                    const label = inputLabelMap[key] || formatKey(key);
                    reviewContainer.append(`
                    <li>
                        <span class="review-label">${label}:</span>
                        <span class="review-value">${value}</span>
                    </li>
                `);
                });
            }
            // Step 2 (Additional Data)
            else if (stepKey !== "Step1" && Object.keys(stepValues).length > 0) {
                hasStep2Data = true;
                Object.entries(stepValues).forEach(([key, value]) => {
                    const label = inputLabelMap[key] || formatKey(key);
                    additionalDataContainer.append(`
                    <li>
                        <span class="review-label">${label}:</span>
                        <span class="review-value">${value}</span>
                    </li>
                `);
                });
            }
        });
        // Remove Step 2 container if empty
        if (!hasStep2Data) {
            additionalDataContainer.closest(".review-section").remove();
        }
        // Handle attachments separately
        if (!hasAttachments) {
            attachmentContainer.closest(".review-section").remove();
        } else {
            // Populate the attachments
            attachments.forEach(att => {
                attachmentContainer.append(`
                <li>
                    <strong>${att.typeName}</strong>: ${att.name}
                </li>
            `);
            });
        }
    }
    

    function formatKey(key) {
        return key.replace(/([A-Z])/g, " $1")
            .replace(/[-_]/g, " ")
            .trim()
            .replace(/\b\w/g, c => c.toUpperCase());
    }
    function clearServiceData(serviceId) {
        localStorage.removeItem(`serviceData_${serviceId}`);
    }
    

});
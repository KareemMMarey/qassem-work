document.addEventListener("DOMContentLoaded", function () {
    // Custom dropdown binding
    bindCustomSelect("nationality");
    bindCustomSelect("prison");
    bindCustomSelect("reason");

    // Hide fields based on serviceCategoryId
    const serviceCategoryId = parseInt(document.getElementById("PR-TR-step2-form").dataset.serviceCategoryId);
    if (serviceCategoryId !== 1) {
        document.getElementById("prison-block")?.classList.add("hidden");
        document.getElementById("reason-block")?.classList.add("hidden");
        document.getElementById("nationality-block")?.classList.add("hidden");
    }

    // Capture on stepper completion
    window.getStep2Data = function () {
        if (!validateStep2()) return null;

        return {
            name: $("#name").val(),
            nationality: $("#nationality").val(),
            idNumber: $("#idNumber").val(),
            birthdate: $("#birthdate").val(),
            phone: $("#phone").val(),
            email: $("#email").val(),
            city: $("#city").val(),
            district: $("#district").val(),
            prison: $("#prison").val(),
            reason: $("#reason").val(),
            details: $("#details").val()
        };
    };
});

function bindCustomSelect(id) {
    const button = document.getElementById(`${id}-button`);
    const dropdown = document.getElementById(`${id}-dropdown`);
    const input = document.getElementById(id);

    if (!button || !dropdown || !input) return;

    const selected = button.querySelector(".selected-value");

    button.addEventListener("click", e => {
        e.preventDefault();
        dropdown.classList.toggle("hidden");
    });

    dropdown.querySelectorAll("li").forEach(item => {
        item.addEventListener("click", () => {
            const value = item.getAttribute("data-value");
            selected.textContent = value;
            input.value = value;
            dropdown.classList.add("hidden");
        });
    });

    document.addEventListener("click", e => {
        if (!button.contains(e.target) && !dropdown.contains(e.target)) {
            dropdown.classList.add("hidden");
        }
    });
}

function validateStep2() {
    let valid = true;
    $(".error").text(""); // reset

    ["name", "idNumber", "birthdate", "phone", "city", "district", "details"].forEach(id => {
        if (!$(`#${id}`).val()) {
            $(`#${id}`).next(".error").text(validationMessages.requiredFieldMessage);
            valid = false;
        }
    });

    if (!$("#nationality").val() && !$("#nationality-block").hasClass("hidden")) {
        $("#nationality").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    if (!$("#prison").val() && !$("#prison-block").hasClass("hidden")) {
        $("#prison").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    if (!$("#reason").val() && !$("#reason-block").hasClass("hidden")) {
        $("#reason").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    return valid;
}

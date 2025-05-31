

    document.addEventListener("DOMContentLoaded", function () {
        // Custom dropdown binding
       

        // Hide fields based on serviceCategoryId
        const serviceCategoryId = parseInt(document.getElementById("PR-TR-step2-form").dataset.serviceCategoryId);
        if (serviceCategoryId !== 1) {
            document.getElementById("prisonToId-block")?.classList.add("hidden");
            document.getElementById("otherDDLId-block")?.classList.add("hidden");
            document.getElementById("countryId-block")?.classList.add("hidden");
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

bindCustomSelect("countryId");
bindCustomSelect("prisonFromId");
bindCustomSelect("prisonToId");
bindCustomSelect("otherDDLId");

function bindCustomSelect(id) {
    const button = document.getElementById(`${id}-button`);
    const dropdown = document.getElementById(`${id}-dropdown`);
    const input = document.getElementById(id);

    console.log(button);
    console.log(dropdown);
    console.log(input);

    if (!button || !dropdown || !input) return;

    const selected = button.querySelector(".selected-value");

    button.addEventListener("click", e => {
        e.preventDefault();
        dropdown.classList.toggle("hidden");
    });

    dropdown.querySelectorAll("li").forEach(item => {
        item.addEventListener("click", () => {
           
            console.log(item);
            const value = item.getAttribute("data-value");
            const text = item.textContent.trim(); // Get the text content (ألبانيا)

            selected.textContent = text; // Display the text instead of the value
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

    ["fullName", "nationalId", "birthdate", "phone", "city", "district", "details"].forEach(id => {
        if (!$(`#${id}`).val()) {
            $(`#${id}`).next(".error").text(validationMessages.requiredFieldMessage);
            valid = false;
        }
    });

    if (!$("#countryId").val() && !$("#countryId-block").hasClass("hidden")) {
        $("#countryId").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    if (!$("#prisonFromId").val() && !$("#prison-from-block").hasClass("hidden")) {
        $("#prisonFromId").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }
    if (!$("#prisonToId").val() && !$("#prison-to-block").hasClass("hidden")) {
        $("#prisonToId").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    if (!$("#otherDDLId").val() && !$("#otherDDLId-block").hasClass("hidden")) {
        $("#otherDDLId").next(".error").text(validationMessages.requiredFieldMessage);
        valid = false;
    }

    return valid;
}


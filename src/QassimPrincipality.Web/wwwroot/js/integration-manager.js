$(document).ready(function () {
    $.get("/Request/GetUserData", function (data) {
        $("#fullName").val(data.fullName);
        $("#nationality").val(data.nationality);
        $("#birthDate").val(data.birthDate);
        $("#idNumber").val(data.idNumber);
        $("#email").val(data.email);
        $("#phone").val(data.phone);
        $("#city").val(data.city);
        $("#district").val(data.district);

        // Set the default requester relation if the dropdown is present
        if ($("#customrequesterRelation").length) {
            $("#customrequesterRelation").val("1"); // Default to "MySelf"
        }
    }).fail(function (xhr) {
        alert("Failed to fetch user data: " + xhr.responseText);
    });


    const customdropdownbutton = $("#custom-dropdown-button");
    const customdropdownList = $("#custom-select-dropdown");
    const customhiddenInput = $("#customrequesterRelation");
    const customselectedValue = $(".custom-selected-value");

    // Toggle dropdown visibility
    customdropdownbutton.on("click", function (event) {
        event.preventDefault();
        const isExpanded = customdropdownbutton.attr("aria-expanded") === "true";
        customdropdownbutton.attr("aria-expanded", !isExpanded);
        customdropdownList.toggleClass("hidden");
    });

    // Handle option selection
    customdropdownList.on("click", "li", function () {
        const selectedText = $(this).text();
        const selectedValueAttr = $(this).data("value");

        customselectedValue.text(selectedText);
        customhiddenInput.val(selectedValueAttr);

        // Close the dropdown
        customdropdownbutton.attr("aria-expanded", "false");
        customdropdownList.addClass("hidden");

        console.log("Selected Value:", selectedValueAttr);
    });

    // Close dropdown on outside click
    $(document).on("click", function (event) {
        if (!$(event.target).closest(".custom-select").length) {
            customdropdownbutton.attr("aria-expanded", "false");
            customdropdownList.addClass("hidden");
        }
    });




    // ----other dropdown 
    const customdropdownbuttonsummons = $("#custom-dropdown-button-summons");
    const summonsdropdownList = $("#custom-select-dropdown-summons");
    const summonshiddenInput = $("#customTypeOfSummons-summons");
    const summonsselectedValue = $(".custom-selected-value-summons");

    // Toggle dropdown visibility
    customdropdownbuttonsummons.on("click", function (event) {
        event.preventDefault();
        const isExpanded = customdropdownbuttonsummons.attr("aria-expanded") === "true";
        customdropdownbuttonsummons.attr("aria-expanded", !isExpanded);
        summonsdropdownList.toggleClass("hidden");
    });

    // Handle option selection
    summonsdropdownList.on("click", "li", function () {
        const selectedText = $(this).text();
        const selectedValueAttr = $(this).data("value");

        summonsselectedValue.text(selectedText);
        summonshiddenInput.val(selectedValueAttr);

        // Close the dropdown
        customdropdownbuttonsummons.attr("aria-expanded", "false");
        summonsdropdownList.addClass("hidden");

        console.log("Selected Summons Type:", selectedValueAttr);
    });
    $(document).on("click", function (event) {
        if (!$(event.target).closest(".custom-select").length) {
            customdropdownbuttonsummons.attr("aria-expanded", "false");
            summonsdropdownList.addClass("hidden");
        }
    });
});
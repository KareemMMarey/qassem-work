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
           // $("#customrequesterRelation").val("1"); // Default to "MySelf"
            $("#customrequesterRelation").val(""); // Default null
        }
    }).fail(function (xhr) {
        alert("Failed to fetch user data: " + xhr.responseText);
    });

    

    //const customdropdownbutton = $("#custom-dropdown-button");
    //const customdropdownList = $("#custom-select-dropdown");
    //const customhiddenInput = $("#customrequesterRelation");
    //const customselectedValue = $(".custom-selected-value");

    // Toggle dropdown visibility
    //customdropdownbutton.on("click", function (event) {
    //    event.preventDefault();
    //    const isExpanded = customdropdownbutton.attr("aria-expanded") === "true";
    //    customdropdownbutton.attr("aria-expanded", !isExpanded);
    //    customdropdownList.toggleClass("hidden");
    //});

    // Handle option selection
    //customdropdownList.on("click", "li", function () {
    //    const selectedText = $(this).text();
    //    const selectedValueAttr = $(this).data("value");

    //    customselectedValue.text(selectedText);
    //    customhiddenInput.val(selectedValueAttr);

    //    // Close the dropdown
    //    customdropdownbutton.attr("aria-expanded", "false");
    //    customdropdownList.addClass("hidden");

    //    console.log("Selected Value:", selectedValueAttr);
    //});

    // Close dropdown on outside click
    //$(document).on("click", function (event) {
    //    if (!$(event.target).closest(".custom-select").length) {
    //        customdropdownbutton.attr("aria-expanded", "false");
    //        customdropdownList.addClass("hidden");
    //    }
    //});




    // ----other dropdown 
    //const customdropdownbuttonsummons = $("#custom-dropdown-button-summons");
    //const summonsdropdownList = $("#custom-select-dropdown-summons");
    //const summonshiddenInput = $("#customTypeOfSummons-summons");
    //const summonsselectedValue = $(".custom-selected-value-summons");

    // Toggle dropdown visibility
    //customdropdownbuttonsummons.on("click", function (event) {
    //    event.preventDefault();
    //    const isExpanded = customdropdownbuttonsummons.attr("aria-expanded") === "true";
    //    customdropdownbuttonsummons.attr("aria-expanded", !isExpanded);
    //    summonsdropdownList.toggleClass("hidden");
    //});

    // Handle option selection
    //summonsdropdownList.on("click", "li", function () {
    //    const selectedText = $(this).text();
    //    const selectedValueAttr = $(this).data("value");

    //    summonsselectedValue.text(selectedText);
    //    summonshiddenInput.val(selectedValueAttr);

    //    // Close the dropdown
    //    customdropdownbuttonsummons.attr("aria-expanded", "false");
    //    summonsdropdownList.addClass("hidden");

    //    console.log("Selected Summons Type:", selectedValueAttr);
    //});
    //$(document).on("click", function (event) {
    //    if (!$(event.target).closest(".custom-select").length) {
    //        customdropdownbuttonsummons.attr("aria-expanded", "false");
    //        summonsdropdownList.addClass("hidden");
    //    }
    //});
});

bindCustomSelect("customrequesterRelation");
bindCustomSelect("customTypeOfSummons");
function bindCustomSelect(id) {
    //alert(id)
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

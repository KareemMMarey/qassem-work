function showPopup(type, title, message) {
    const popupOverlay = $("#global-popup");
    const popupIcon = $("#popup-icon");
    const popupTitle = $("#popup-title");
    const popupMessage = $("#popup-message");

    // Set the styles based on the type
    popupOverlay.removeClass("popup-success popup-error popup-warning popup-info");
    popupIcon.removeClass("popup-success popup-error popup-warning popup-info");

    switch (type) {
        case "success":
            popupIcon.html("✔️").addClass("popup-success");
            break;
        case "error":
            popupIcon.html("❌").addClass("popup-error");
            break;
        case "warning":
            popupIcon.html("⚠️").addClass("popup-warning");
            break;
        case "info":
            popupIcon.html("ℹ️").addClass("popup-info");
            break;
    }

    // Set the title and message
    popupTitle.text(title);
    popupMessage.text(message);

    // Show the popup
    popupOverlay.fadeIn();
}

// Close the popup
$("#popup-close-btn").click(function () {
    $("#global-popup").fadeOut();
});
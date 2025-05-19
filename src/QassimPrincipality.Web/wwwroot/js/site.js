// ------------------------------- Header -------------------------------
if (document.querySelector(".pc-header")) {
    const currentCulture = document.documentElement.lang || "ar-SA";
    console.log(currentCulture);
        function updateDate() {
        const today = new Date();
        const year = today.getFullYear();
        const day = today.getDate();

        // Format the month based on culture
        const month = today.toLocaleString(currentCulture, { month: 'long' });

        // Format the date based on culture direction
        let formattedDate;
        if (currentCulture.startsWith("ar")) {
            formattedDate = `${day} - ${month} - ${year}`;
        } else {
            formattedDate = `${month} ${day}, ${year}`;
        }

        document.getElementById('current-date').textContent = formattedDate;
    }

    // Current time
    function updateTime() {
        const now = new Date();
        const isArabic = currentCulture.startsWith("ar");

        let hours = now.getHours();
        const minutes = now.getMinutes().toString().padStart(2, '0');
        let period = "AM";

        // Handle 12-hour clock formatting
        if (hours >= 12) {
            period = isArabic ? "مساءً" : "PM";
            hours = hours > 12 ? hours - 12 : hours;
        } else {
            period = isArabic ? "صباحاً" : "AM";
            hours = hours === 0 ? 12 : hours;
        }

        const currentTime = isArabic ? `${hours}:${minutes} ${period}` : `${hours}:${minutes} ${period}`;
        document.getElementById('current-time').textContent = currentTime;
    }


    // Initialize and update time every minute
    // Initialize date and time on page load
    updateDate();
    updateTime();
    setInterval(updateTime, 60000);

    // Menu toggle
    const navMenu = document.getElementById("nav-menu");
    const menuToggle = document.getElementById("menu-toggle");
    const closeMenuToggle = document.getElementById("close-menu-toggle");

    menuToggle.addEventListener("click", function (e) {
        e.preventDefault();

        navMenu.classList.toggle("active");
        menuToggle.style.display = "none";
        closeMenuToggle.style.display = "block";
    });

    closeMenuToggle.addEventListener("click", function (e) {
        e.preventDefault();

        navMenu.classList.remove("active");
        menuToggle.style.display = "block";
        closeMenuToggle.style.display = "none";
    });
}





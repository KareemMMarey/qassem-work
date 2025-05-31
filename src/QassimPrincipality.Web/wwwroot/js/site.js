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



// ------------------------------- Service Page -------------------------------

if (document.getElementById("pcServicePage")) {
    const serviceRequirementsBtn = document.getElementById("pc-service-requirements-btn");
    const serviceRequirements = document.querySelector(".pc-service-requirements");
    const serviceStepsBtn = document.getElementById("pc-service-steps-btn");
    const serviceSteps = document.querySelector(".pc-service-steps");
    const serviceFAQBtn = document.getElementById("pc-service-FAQ-btn");
    const serviceFAQ = document.querySelector(".pc-service-FAQ");

    const buttons = document.querySelectorAll(".pc-service-info-divisions button");

    // Function to remove 'active' class from all buttons
    function clearActiveStates() {
        buttons.forEach(button => button.classList.remove("active"));
    }

    // Initially set serviceRequirements as active
    serviceRequirementsBtn.classList.add("active");
    serviceRequirements.style.display = "flex";
    serviceSteps.style.display = "none";
    serviceFAQ.style.display = "none";

    // Show/Hide sections and set active state
    serviceRequirementsBtn.addEventListener("click", () => {
        clearActiveStates();
        serviceRequirementsBtn.classList.add("active");
        serviceRequirements.style.display = "flex";
        serviceSteps.style.display = "none";
        serviceFAQ.style.display = "none";
    });

    serviceStepsBtn.addEventListener("click", () => {
        clearActiveStates();
        serviceStepsBtn.classList.add("active");
        serviceRequirements.style.display = "none";
        serviceSteps.style.display = "flex";
        serviceFAQ.style.display = "none";
    });

    serviceFAQBtn.addEventListener("click", () => {
        clearActiveStates();
        serviceFAQBtn.classList.add("active");
        serviceRequirements.style.display = "none";
        serviceSteps.style.display = "none";
        serviceFAQ.style.display = "flex";
    });
}


// ------------------------------- FAQ's -------------------------------
if (document.querySelector(".pc-service-FAQ")) {
    document.querySelectorAll(".pc-accordion").forEach(accordion => {
        const accordionHead = accordion.querySelector(".pc-accordion-head");
        const accordionBody = accordion.querySelector(".pc-accordion-body");
        const arrowIcon = accordionHead.querySelector("img");

        accordionHead.addEventListener("click", () => {
            accordionBody.classList.toggle("open");

            if (accordionBody.classList.contains("open")) {
                arrowIcon.style.transform = "rotate(180deg)";
            } else {
                arrowIcon.style.transform = "rotate(0deg)";
            }
        });
    });
}

// ------------------------------- OTP timer -------------------------------
if (document.getElementById("timer")) {
    const timerElement = document.getElementById("timer");

    if (timerElement) {
        let timeRemaining = 60;

        function updateTimer() {
            const minutes = Math.floor(timeRemaining / 60);
            const seconds = timeRemaining % 60;

            timerElement.textContent = `${minutes}:${seconds.toString().padStart(2, '0')}`;

            timeRemaining--;

            if (timeRemaining < 0) {
                clearInterval(timerInterval);
                timerElement.textContent = "0:00";
            }
        }

        const timerInterval = setInterval(updateTimer, 1000);
        updateTimer();
    }
}
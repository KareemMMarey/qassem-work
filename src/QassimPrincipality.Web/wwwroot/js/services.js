

if (document) {
    document.addEventListener("DOMContentLoaded", function () {
        const tabs = document.querySelectorAll(".pc-tab");
        const serviceCards = document.querySelectorAll(".pc-card");

        tabs.forEach(tab => {
            tab.addEventListener("click", () => {
                const filter = tab.getAttribute("data-filter");

                // Set active class
                tabs.forEach(t => t.classList.remove("active"));
                tab.classList.add("active");

                // Show/Hide services based on the selected category
                serviceCards.forEach(card => {
                    const category = card.getAttribute("data-category");

                    console.log(category);
                    console.log(filter);

                    if (filter === "all" || filter === category) {
                        console.log(category);
                        card.style.display = "block";
                        console.log('else');
                    } else {
                        console.log('else');
                        card.style.display = "none";
                    }
                });
            });
        });

        // Trigger the "all" tab by default
        document.querySelector(".pc-tab[data-filter='all']").click();
    });

    document.addEventListener("DOMContentLoaded", function () {
        const searchInput = document.getElementById("service-search");
        const clearSearchBtn = document.getElementById("clear-search-btn");
        const clearSearchFieldBtn = document.getElementById("clear-search-field-btn");
        const servicesContainer = document.getElementById("services-container");
        const noResults = document.getElementById("no-results");

        function filterServices() {
            const query = searchInput.value.trim().toLowerCase();
            const cards = servicesContainer.querySelectorAll(".pc-card");
            let hasResults = false;

            cards.forEach(card => {
                const serviceName = card.getAttribute("data-name").toLowerCase();
                if (serviceName.includes(query)) {
                    card.style.display = "block";
                    hasResults = true;
                } else {
                    card.style.display = "none";
                }
            });

            // Show or hide the empty state
            noResults.style.display = hasResults ? "none" : "block";
        }

        // Handle typing in the search field
        searchInput.addEventListener("input", filterServices);

        // Clear search field
        clearSearchBtn.addEventListener("click", () => {
            searchInput.value = "";
            filterServices();
            searchInput.focus();
        });

        // Clear search using the button in the empty state
        clearSearchFieldBtn.addEventListener("click", () => {
            searchInput.value = "";
            filterServices();
            searchInput.focus();
        });
    });
}


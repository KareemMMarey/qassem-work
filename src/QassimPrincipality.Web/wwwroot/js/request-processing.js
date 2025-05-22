if (document.getElementById("search-my-requests-internal")) {
    document.getElementById("search-my-requests-internal").addEventListener("click", function (e) {
        e.preventDefault();
        const searchText = $('#request-search-input').val().trim();
        const filter = {
            requestNumber: searchText,

        };

        $.post({
            url: '/Request/SearchRequests',
            contentType: 'application/json',
            data: JSON.stringify(filter),
            success: function (data) {
                console.log(data); // Process result
            },
            error: function (xhr) {
                alert("Error searching requests: " + xhr.responseText);
            }
        });

    });

    
}
if (document.getElementById("search-my-requests")) {
    document.getElementById("search-my-requests").addEventListener("click", function (e) {
        e.preventDefault();

        const filter = {
            userId: this.dataset.userId,
            serviceId: parseInt(this.dataset.serviceId),
            status: parseInt(this.dataset.status),
            startDate: this.dataset.startDate,
            endDate: this.dataset.endDate
        };

        console.log(filter);
        // Use $.post with JSON serialization
        $.post({
            url: '/Request/SearchRequests',
            contentType: 'application/json',
            data: JSON.stringify({
                userId: this.dataset.userId,
            }),
            success: function (data) {
                console.log(data); // Process result
            },
            error: function (xhr) {
                alert("Error searching requests: " + xhr.responseText);
            }
        });

    });
}

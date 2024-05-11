// for demo purpose
//const loginBtn = document.getElementById("loginBtn");
//const userMenu = document.getElementById("userMenu");

//loginBtn.addEventListener('click', (event) => {
//	event.preventDefault();
//	loginBtn.classList.add('d-none');
//	userMenu.classList.remove('d-none');
//})


const filterInput = document.getElementById("filterInput");

if(filterInput){
	const wrapper = document.querySelector(".services-list");
	const cards = wrapper.querySelectorAll(".services-list .services-card-wrapper");
	const serviceEmptyState = document.getElementById("serviceEmptyState");
	filterInput.addEventListener("input", function () {
		const filterText = this.value.toLowerCase().normalize("NFD"); 
		let isAnyMatch = false; 
	
		for (const card of cards) {
			const title = card.querySelector(".service-card__title").textContent.toLowerCase(); 
			const isMatch = title.indexOf(filterText) !== -1; 
	
			isMatch ? card.classList.remove('services-card-wrapper--hidden') : card.classList.add('services-card-wrapper--hidden'); 
			
			isAnyMatch = isAnyMatch || isMatch; 
			isAnyMatch? serviceEmptyState.classList.remove("service-list-empty--shown") : serviceEmptyState.classList.add("service-list-empty--shown")
	
		}
	});
}

var starrating = new StarRating('#serviceRating', {tooltip:false});
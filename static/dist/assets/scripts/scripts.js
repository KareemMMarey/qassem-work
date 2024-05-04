// for demo purpose
const loginBtn = document.getElementById("loginBtn");
const userMenu = document.getElementById("userMenu");

loginBtn.addEventListener('click', (event) => {
	event.preventDefault();
	loginBtn.classList.add('d-none');
	userMenu.classList.remove('d-none');
})
(function () {
	const swiper = new Swiper('#homeSlider', {
		allowTouchMove: false,
		loop: false,
		autoplay: false,
		parallax: true,
		mousewheel: {
			invert: false,
		},
		effect: 'fade',
		fadeEffect: {
			crossFade: true
		},
		pagination: {
			el: '#homeSliderPafination',
			type: "progressbar",
		},
		on: {
			init: function () {
				getSlideName(this);
			},
			slideChange: function () {
				getSlideName(this);
			},
		},
	});

	const internSlider = new Swiper('#internSlider', {
		slidesPerView: 4.6,
		spaceBetween: 30,
		loop: false,

		navigation: {
			nextEl: '.swiper-button-next',
			prevEl: '.swiper-button-prev',
		},

	});

	const instructionsSlider = new Swiper('#instructionsSlider', {
		slidesPerView: 3.2,
		spaceBetween: 30,
		loop: false,

		navigation: {
			nextEl: '.swiper-button-next',
			prevEl: '.swiper-button-prev',
		},

	});

	var s = skrollr.init({
		smoothScrolling: false,
		forceHeight: false
	});
})();

/**
 * get slide name from attribute data-title
 * @param {*} el the slider
 */
function getSlideName(el) {
	const currentIndex = el.activeIndex + 1;
	const nextSlide = el.slides[currentIndex];
	const slideName = nextSlide ? nextSlide.getAttribute("data-title") : '';
	document.getElementById("slideName").innerText = slideName;
}

// mega menu
if (document.getElementById("megaMenu")) {
	const openMenu = document.getElementById("openMenu");
	const closeMenu = document.getElementById("closeMenu");
	const megaMenu = document.getElementById("megaMenu");

	openMenu.addEventListener("click", () => {
		megaMenu.classList.remove('mega-menu--hidden');
	});
	closeMenu.addEventListener("click", () => {
		megaMenu.classList.add('mega-menu--hidden');
	});
}

// for demo purpose
if (document.getElementById("notificationBox")) {
	const notificationBox = document.getElementById("notificationBox");
	const closeNotificationBox = document.getElementById("closeNotificationBox");

	setTimeout(() => {
		notificationBox.classList.remove('home__notifications--hidden');
	}, 3000);


	closeNotificationBox.addEventListener("click", () => {
		notificationBox.classList.add('home__notifications--hidden');
	});
}

// tabs
const tabBtns = document.querySelectorAll(".tabs-btn");
const tabsContent = document.querySelectorAll(".tabs-content");

tabBtns.forEach((btn) => {
	btn.addEventListener('click', (e) => {
		tabBtns.forEach((btn) => {
			btn.classList.remove("tabs-btn--active");
		});
		e.currentTarget.classList.add("tabs-btn--active");
		const tabName = e.target.id
		showTabsContnet(tabName)
	});
});
function showTabsContnet(id) {
	tabsContent.forEach((tab) => {
		tab.classList.remove("tabs-content--show");
		if (tab.getAttribute("tabTarget") === id) {
			tab.classList.add("tabs-content--show");
		}
	});

}
// ///////////////////////////////////////////////////////////
// for accordion border
const accordionBtn = document.querySelectorAll(".accordion-button");
const accordionItems = document.querySelectorAll(".accordion-item");
accordionBtn.forEach((btn) => {
	btn.addEventListener('click', (e) => {
		accordionItems.forEach((accordionItem) => {
			accordionItem.classList.remove("accordion-item--show");
		});
		e.target.parentNode.parentNode.classList.add("accordion-item--show");
	})
});

const countries = {
	Riyadh: {
		title: 'الرياض',
		isShown: true,
		locations: [
			{
				title: 'الرياض',
				address: 'واجهة الرياض للأعمال مبنى N10',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}, {
				title: 'غرفة الرياض',
				address: 'حي الملك عبدالله، طريق الملك عبدالله بن عبدالعزيز',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg', 'logo-layer2.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}, {
				title: 'التعليم',
				address: 'صلاح الدين، طريق الملك عبدالله',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg', 'logo-moh.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			},
		]
	},
	Makkah: {
		title: 'مكة',
		isShown: true,
		locations: [
			{
				title: 'مكة',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}, {
				title: 'مكة',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Aldammam: {
		title: 'الدمام',
		isShown: true,
		locations: [
			{
				title: 'الدمام',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}, {
				title: 'الدمام',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Abha: {
		title: 'أبها',
		isShown: true,
		locations: [
			{
				title: 'أبها',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Buraydah: {
		title: 'بريدة',
		isShown: true,
		locations: [
			{
				title: 'بريدة',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Tabuk: {
		title: 'تبوك',
		isShown: true,
		locations: [
			{
				title: 'تبوك',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	ALjouf: {
		title: 'الجوف',
		isShown: true,
		locations: [
			{
				title: 'الجوف',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Alahsa: {
		title: 'الأحساء',
		isShown: true,
		locations: [
			{
				title: 'الأحساء',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Taif: {
		title: 'الطائف',
		isShown: true,
		locations: [
			{
				title: 'الطائف',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Madinah: {
		title: 'المدينة المنورة',
		isShown: true,
		locations: [
			{
				title: 'المدينة المنورة',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Hail: {
		title: 'حائل',
		isShown: true,
		locations: [
			{
				title: 'حائل',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Najran: {
		title: 'نجران',
		isShown: true,
		locations: [
			{
				title: 'نجران',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	},
	Jazan: {
		title: 'جازان',
		isShown: true,
		locations: [
			{
				title: 'جازان',
				address: 'حي النهضه، طريق الملك عبد العزيز الفرعي',
				workingDays: 'من الأحد إلى الخميس يوم السبت',
				hoursFrom: '9 ص - 8 م',
				hoursTo: '10 ص - 4 م',
				logos: ['logo-sbc.svg'],
				location: 'https://maps.app.goo.gl/pXH9G84ufYh6QDzV6'
			}
		]
	}
}

const regions = document.querySelectorAll('.map__region');
regions.forEach(region => {
	region.addEventListener('click', function () {
		const cityAlias = this.getAttribute('id');
		const cityData = countries[cityAlias];
		displayCityDetails(cityData, this);
	})
})

function displayCityDetails(cityDetails, elem) {
	// Check if cityDetails is valid
	if (!cityDetails || cityDetails.isShown == false) return;

	regions.forEach(region => {
		region.classList.remove('map__region--active');
	});

	elem.classList.add('map__region--active');
	document.querySelector('.map-regions').classList.remove('map-regions--hidden');

	// Start building the HTML for the details
	let detailsHTML = `<h2 class="map-regions__title animate animate__fadeIn">المراكز و الفروع</h2><div class="map-regions__locations">`;

	// Loop through each location in the city
	cityDetails.locations.forEach(location => {
		// Create HTML for logos
		let logosHTML = location.logos.map(logo => `<div class='map-location__logo'><img src="./assets/images/${logo}" alt="Logo"></div>`).join('');

		detailsHTML += `
					<div class="map-location animate animate__fadeInDown">
							<div class="map-location__row">
									<div class="map-location__cell">
											<span class="map-location__icon">${createIcon('building2')}</span>
											<div class="map-location__info">
													<div class="map-location__title">${location.title}</div>
													<div class="map-location__value">${location.address}</div>
											</div>
									</div>
									<div class="map-location__cell map-location__logos">
											${logosHTML}
									</div>
							</div>
							<div class="map-location__row">
									<div class="map-location__cell">
											<span class="map-location__icon">${createIcon('calendar2')}</span>
											<div class="map-location__info">
													<div class="map-location__value">${location.hoursFrom}</div>
													<div class="map-location__value">${location.hoursTo}</div>
											</div>
									</div>
							</div>
							<div class="map-location__row map-location__link">
									<div class="map-location__cell">
											<a href="${location.location}" class="btn btn-secondary" target="_blank"><span>الموقغ</span> ${createIcon('pin')}</a>
									</div>
							</div>
					</div>`;
	});

	detailsHTML += `</div>`;

	// Find the container where the details should be displayed and set its innerHTML
	const detailsContainer = document.getElementById('cityInfo');
	if (detailsContainer) {
		detailsContainer.innerHTML = detailsHTML;
	}
}

function createIcon(icon) {
	// Create the span element
	const span = document.createElement('span');
	span.className = 'city-info__label';

	// Create the SVG element
	const svg = document.createElementNS('http://www.w3.org/2000/svg', 'svg');

	// Create the use element
	const use = document.createElementNS('http://www.w3.org/2000/svg', 'use');
	use.setAttributeNS('http://www.w3.org/1999/xlink', 'xlink:href', `./assets/images/svg-icons.svg#${icon}`);

	// Append the use element to the SVG, and the SVG to the span
	svg.appendChild(use);
	span.appendChild(svg);
	return span.innerHTML;
}

// const bannerTitle = document.getElementById('bannerTitleContainer');
// const bannerTitleTop = getOffsetTop(bannerTitle);
// const banner = document.querySelector('#banner');
// const header = document.querySelector('#mainHeader');

// window.onscroll = function () {
// 	var scrollPosition = document.documentElement.scrollTop || document.body.scrollTop;
// 	if (banner && bannerTitle) {
// 		if (scrollPosition >= 10) {
// 			banner.classList.add('banner--fixed');
// 		} else {
// 			banner.classList.remove('banner--fixed');
// 		}
// 	}
// };


// function getOffsetTop(element) {
// 	if (!element)
// 		return
// 	var rect = element.getBoundingClientRect();
// 	var scrollTop = window.scrollY || window.pageYOffset;
// 	return rect.top + scrollTop;
// }

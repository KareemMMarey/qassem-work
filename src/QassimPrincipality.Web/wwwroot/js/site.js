// ------------------------------- Header -------------------------------
if (document.querySelector(".pc-header")) {
    const currentCulture = document.documentElement.lang || "ar-SA";
    console.log(currentCulture);
    //// Current date
    //const today = new Date();
    //const year = today.getFullYear();
    //const month = today.toLocaleString('ar-EG', { month: 'long' });
    //const day = today.getDate();
    //const formattedDate = `${day} - ${month} - ${year}`;

    //document.getElementById('current-date').textContent = formattedDate;

    //// Current time
    //function updateTime() {
    //    const now = new Date();
    //    let hours = now.getHours();
    //    const minutes = now.getMinutes().toString().padStart(2, '0');
    //    const period = hours >= 12 ? 'مساءً' : 'صباحاً';

    //    hours = hours % 12;
    //    hours = hours ? hours : 12;

    //    const currentTime = `${hours}:${minutes} ${period}`;
    //    document.getElementById('current-time').textContent = currentTime;
    //}

    //// Initialize and update time every minute
    //updateTime();
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

// ------------------------------- Forms validation -------------------------------
const setError = (element, message) => {
    const inputControl = element.closest('.pc-checkbox-container') || element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    // Clear existing icon and message
    while (errorDisplay.firstChild) {
        errorDisplay.removeChild(errorDisplay.firstChild);
    }

    const icon = document.createElement('img');
    icon.src = '/icons/warning.svg';
    icon.alt = 'warning';
    icon.width = 16;
    icon.height = 16;
    icon.style.marginLeft = '8px';

    const validationMessage = document.createElement('p');
    validationMessage.textContent = message;
    validationMessage.style.display = 'inline';
    validationMessage.style.color = '#B42318';
    validationMessage.style.fontSize = '14px';

    errorDisplay.appendChild(icon);
    errorDisplay.appendChild(validationMessage);

    inputControl.classList.add('error');
}

const removeError = element => {
    const inputControl = element.closest('.pc-checkbox-container') || element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    // Clear existing icon and message
    while (errorDisplay.firstChild) {
        errorDisplay.removeChild(errorDisplay.firstChild);
    }

    inputControl.classList.remove('error');
};

// Login form
if (document.getElementById("loginForm")) {
    const loginForm = document.getElementById('loginForm');
    const idNumber = document.getElementById('idNumber');

    loginForm.addEventListener('submit', e => {
        e.preventDefault();
        const idNumberValue = idNumber.value.trim();

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم بطاقة الأحوال / الإقامة');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم بطاقة أحوال / إقامة صحيح');
        } else {
            removeError(idNumber);
            loginForm.submit();
            window.location.href = '/Login/OTP';
        }
    });
}

// Admin Login form
if (document.getElementById("AdminLoginForm")) {
    const AdminLoginForm = document.getElementById('AdminLoginForm');
    const username = document.getElementById('username');
    const password = document.getElementById('password');

    AdminLoginForm.addEventListener('submit', e => {
        e.preventDefault();
        const usernameValue = username.value.trim();
        const passwordValue = password.value.trim();
        let isValid = true;

        if (usernameValue === '') {
            setError(username, 'يرجى إدخال اسم المستخدم');
            isValid = false;
        } else {
            removeError(username);
            isValid = true;
        }

        if (passwordValue === '') {
            setError(password, 'يرجى إدخال كلمة المرور');
        } else {
            removeError(password);
            isValid = true;
        }

        if (isValid) {
            AdminLoginForm.submit();
            window.location.href = '/Admin/AllRequests';
        }
    });
}

// Account setup form
if (document.getElementById("accountSetupForm")) {
    const accountSetupForm = document.getElementById('accountSetupForm');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const acceptTerms = document.getElementById('acceptTerms');

    accountSetupForm.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (!acceptTerms.checked) {
            setError(acceptTerms, 'يجب الموافقة على الشروط والأحكام');
            isValid = false;
        } else {
            removeError(acceptTerms);
        }

        if (isValid) {
            accountSetupForm.submit();
            window.location.href = '/Home/Index';
        }
    });
}

// Edit Contact Info form
if (document.querySelector(".pc-profile")) {
    const editContactInfobtn = document.getElementById('editContactInfobtn');
    const cancel = document.getElementById('cancel');
    const contactInfoContainer = document.getElementById('contactInfoContainer');
    const editContactInfoForm = document.getElementById('editContactInfoForm');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');

    editContactInfobtn.addEventListener("click", () => {
        editContactInfoForm.style.display = "flex";
        contactInfoContainer.style.display = "none";
        editContactInfobtn.style.display = "none";
    });

    cancel.addEventListener("click", e => {
        e.preventDefault();

        editContactInfoForm.style.display = "none";
        contactInfoContainer.style.display = "grid";
        editContactInfobtn.style.display = "flex";
    });

    editContactInfoForm.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (isValid) {
            editContactInfoForm.submit();
            editContactInfoForm.style.display = "none";
        }
    });
}

// TQ Service form
if (document.getElementById("transactionQueryForm")) {
    const transactionQueryForm = document.getElementById('transactionQueryForm');
    const transactionNumber = document.getElementById('transactionNumber');
    const idNumber = document.getElementById('idNumber');

    transactionQueryForm.addEventListener('submit', e => {
        e.preventDefault();

        const transactionNumberValue = transactionNumber.value.trim();
        const idNumberValue = idNumber.value.trim();

        if (transactionNumberValue === '') {
            setError(transactionNumber, 'يرجى إدخال رقم المعاملة');
        } else if (!/^\d*$/.test(transactionNumberValue)) {
            setError(transactionNumber, 'يرجى إدخال أرقام فقط');
        } else if (transactionNumberValue.length < 6 || transactionNumberValue.length > 6) {
            setError(transactionNumber, 'يرجى إدخال رقم معاملة صحيح');
        } else {
            removeError(transactionNumber);
        }

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم الهوية');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم هوية صحيح');
        } else {
            removeError(idNumber);
        }

    });
}

// Send request
if (document.getElementById("sendRequest")) {
    const sendRequestBtn = document.getElementById('sendRequest');
    const acceptTerms = document.getElementById('acceptTerms');

    sendRequestBtn.addEventListener('click',() => {
        let isValid = true;

        if (!acceptTerms.checked) {
            setError(acceptTerms, 'يجب الموافقة على الشروط والأحكام');
        } else {
            removeError(acceptTerms);
            window.location.href = '/Home/SuccessRequest';
        }
    });
}

// PR-TR Service forms
if (document.getElementById("PR-TR-step1-form")) {
    const step1Form = document.getElementById('PR-TR-step1-form');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');

    const dropdownButton = document.getElementById("dropdown-button");
    const dropdown = document.getElementById("select-dropdown");
    const selectedValueSpan = dropdownButton.querySelector(".selected-value");
    let applicantRole = "";

    dropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            applicantRole = option.textContent.trim();
            selectedValueSpan.textContent = applicantRole;
            removeError(dropdownButton);
        });
    });

    step1Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        let isValid = true;

        if (!applicantRole || applicantRole === "اختر...") {
            setError(dropdownButton, "يرجى اختيار صفة مقدم الطلب");
            isValid = false;
        } else {
            removeError(dropdownButton);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (isValid) {
            const formData = new FormData(step1Form);
            formData.append('applicantRole', applicantRole);

            fetch(step1Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTR/StepTwo';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("PR-TR-step2-form-a")) {
    const step2Form = document.getElementById('PR-TR-step2-form-a');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const details = document.getElementById('details');

    const prisonDropdownButton = document.getElementById("prison-dropdown-button");
    const prisonDropdown = document.getElementById("prison-select-dropdown");
    const prisonSelectedValueSpan = prisonDropdownButton.querySelector(".selected-value");
    let prison = "";

    prisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            prison = option.textContent.trim();
            prisonSelectedValueSpan.textContent = prison;
            removeError(prisonDropdownButton);
        });
    });

    const reasonDropdownButton = document.getElementById("reason-dropdown-button");
    const reasonDropdown = document.getElementById("reason-select-dropdown");
    const reasonSelectedValueSpan = reasonDropdownButton.querySelector(".selected-value");
    let reason = "";

    reasonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            reason = option.textContent.trim();
            reasonSelectedValueSpan.textContent = reason;
            removeError(reasonDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (!prison || prison === "اختر...") {
            setError(prisonDropdownButton, "يرجى اختيار السجن");
            isValid = false;
        } else {
            removeError(prisonDropdownButton);
        }

        if (!reason || reason === "اختر...") {
            setError(reasonDropdownButton, "يرجى اختيار سبب طلب الخروج المؤقت");
            isValid = false;
        } else {
            removeError(reasonDropdownButton);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            const formData = new FormData(step2Form);
            formData.append('nationality', nationality);
            formData.append('prison', prison);
            formData.append('reason', reason);

            fetch(step2Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTR/StepThree';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("PR-TR-step2-form")) {
    const step2Form = document.getElementById('PR-TR-step2-form');
    const name = document.getElementById('name');
    const idNumber = document.getElementById('idNumber');
    const birthdate = document.getElementById('birthdate');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const city = document.getElementById('city');
    const district = document.getElementById('district');
    const details = document.getElementById('details');

    const nationalityDropdownButton = document.getElementById("nationality-dropdown-button");
    const nationalityDropdown = document.getElementById("nationality-select-dropdown");
    const nationalitySelectedValueSpan = nationalityDropdownButton.querySelector(".selected-value");
    let nationality = "";

    nationalityDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            nationality = option.textContent.trim();
            nationalitySelectedValueSpan.textContent = nationality;
            removeError(nationalityDropdownButton);
        });
    });

    const prisonDropdownButton = document.getElementById("prison-dropdown-button");
    const prisonDropdown = document.getElementById("prison-select-dropdown");
    const prisonSelectedValueSpan = prisonDropdownButton.querySelector(".selected-value");
    let prison = "";

    prisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            prison = option.textContent.trim();
            prisonSelectedValueSpan.textContent = prison;
            removeError(prisonDropdownButton);
        });
    });

    const reasonDropdownButton = document.getElementById("reason-dropdown-button");
    const reasonDropdown = document.getElementById("reason-select-dropdown");
    const reasonSelectedValueSpan = reasonDropdownButton.querySelector(".selected-value");
    let reason = "";

    reasonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            reason = option.textContent.trim();
            reasonSelectedValueSpan.textContent = reason;
            removeError(reasonDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const nameValue = name.value.trim();
        const idNumberValue = idNumber.value.trim();
        const birthdateValue = birthdate.value;
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const cityValue = city.value.trim();
        const districtValue = district.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (nameValue === '') {
            setError(name, 'يرجى إدخال الاسم الرباعي');
            isValid = false;
        } else {
            removeError(name);
        }

        if (!nationality || nationality === "اختر...") {
            setError(nationalityDropdownButton, "يرجى اختيار الجنسية");
            isValid = false;
        } else {
            removeError(nationalityDropdownButton);
        }

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم الهوية');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم هوية صحيح');
        } else {
            removeError(idNumber);
        }

        if (!birthdateValue) {
            setError(birthdate, "يرجى تحديد تاريخ الميلاد");
            isValid = false;
        } else {
            removeError(birthdate);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (cityValue === '') {
            setError(city, 'يرجى إدخال المدينة');
            isValid = false;
        } else {
            removeError(city);
        }

        if (districtValue === '') {
            setError(district, 'يرجى إدخال الحي');
            isValid = false;
        } else {
            removeError(district);
        }

        if (!prison || prison === "اختر...") {
            setError(prisonDropdownButton, "يرجى اختيار السجن");
            isValid = false;
        } else {
            removeError(prisonDropdownButton);
        }

        if (!reason || reason === "اختر...") {
            setError(reasonDropdownButton, "يرجى اختيار سبب طلب الخروج المؤقت");
            isValid = false;
        } else {
            removeError(reasonDropdownButton);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            const formData = new FormData(step2Form);
            formData.append('nationality', nationality);
            formData.append('prison', prison);
            formData.append('reason', reason);

            fetch(step2Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTR/StepThree';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

// PR-TF Service forms
if (document.getElementById("PR-TF-step1-form")) {
    const step1Form = document.getElementById('PR-TF-step1-form');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');

    const dropdownButton = document.getElementById("dropdown-button");
    const dropdown = document.getElementById("select-dropdown");
    const selectedValueSpan = dropdownButton.querySelector(".selected-value");
    let applicantRole = "";

    dropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            applicantRole = option.textContent.trim();
            selectedValueSpan.textContent = applicantRole;
            removeError(dropdownButton);
        });
    });

    step1Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        let isValid = true;

        if (!applicantRole || applicantRole === "اختر...") {
            setError(dropdownButton, "يرجى اختيار صفة مقدم الطلب");
            isValid = false;
        } else {
            removeError(dropdownButton);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (isValid) {
            const formData = new FormData(step1Form);
            formData.append('applicantRole', applicantRole);

            fetch(step1Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTF/StepTwo';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("PR-TF-step2-form-a")) {
    const step2Form = document.getElementById('PR-TF-step2-form-a');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const details = document.getElementById('details');

    const currentPrisonDropdownButton = document.getElementById("current-prison-dropdown-button");
    const currentPrisonDropdown = document.getElementById("current-prison-select-dropdown");
    const currentPrisonSelectedValueSpan = currentPrisonDropdownButton.querySelector(".selected-value");
    let currentPrison = "";

    currentPrisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            currentPrison = option.textContent.trim();
            currentPrisonSelectedValueSpan.textContent = currentPrison;
            removeError(currentPrisonDropdownButton);
        });
    });

    const destinationPrisonDropdownButton = document.getElementById("destination-prison-dropdown-button");
    const destinationPrisonDropdown = document.getElementById("destination-prison-select-dropdown");
    const destinationPrisonSelectedValueSpan = destinationPrisonDropdownButton.querySelector(".selected-value");
    let destinationPrison = "";

    destinationPrisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            destinationPrison = option.textContent.trim();
            destinationPrisonSelectedValueSpan.textContent = destinationPrison;
            removeError(destinationPrisonDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (!currentPrison || currentPrison === "اختر...") {
            setError(currentPrisonDropdownButton, "يرجى اختيار السجن");
            isValid = false;
        } else {
            removeError(currentPrisonDropdownButton);
        }

        if (!destinationPrison || destinationPrison === "اختر...") {
            setError(destinationPrisonDropdownButton, "يرجى اختيار سبب طلب الخروج المؤقت");
            isValid = false;
        } else {
            removeError(destinationPrisonDropdownButton);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            const formData = new FormData(step2Form);
            formData.append('nationality', nationality);
            formData.append('currentPrison', currentPrison);
            formData.append('destinationPrison', destinationPrison);

            fetch(step2Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTF/StepThree';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("PR-TF-step2-form")) {
    const step2Form = document.getElementById('PR-TF-step2-form');
    const name = document.getElementById('name');
    const idNumber = document.getElementById('idNumber');
    const birthdate = document.getElementById('birthdate');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const city = document.getElementById('city');
    const district = document.getElementById('district');
    const details = document.getElementById('details');

    const nationalityDropdownButton = document.getElementById("nationality-dropdown-button");
    const nationalityDropdown = document.getElementById("nationality-select-dropdown");
    const nationalitySelectedValueSpan = nationalityDropdownButton.querySelector(".selected-value");
    let nationality = "";

    nationalityDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            nationality = option.textContent.trim();
            nationalitySelectedValueSpan.textContent = nationality;
            removeError(nationalityDropdownButton);
        });
    });

    const currentPrisonDropdownButton = document.getElementById("current-prison-dropdown-button");
    const currentPrisonDropdown = document.getElementById("current-prison-select-dropdown");
    const currentPrisonSelectedValueSpan = currentPrisonDropdownButton.querySelector(".selected-value");
    let currentPrison = "";

    currentPrisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            currentPrison = option.textContent.trim();
            currentPrisonSelectedValueSpan.textContent = currentPrison;
            removeError(currentPrisonDropdownButton);
        });
    });

    const destinationPrisonDropdownButton = document.getElementById("destination-prison-dropdown-button");
    const destinationPrisonDropdown = document.getElementById("destination-prison-select-dropdown");
    const destinationPrisonSelectedValueSpan = destinationPrisonDropdownButton.querySelector(".selected-value");
    let destinationPrison = "";

    destinationPrisonDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            destinationPrison = option.textContent.trim();
            destinationPrisonSelectedValueSpan.textContent = destinationPrison;
            removeError(destinationPrisonDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const nameValue = name.value.trim();
        const idNumberValue = idNumber.value.trim();
        const birthdateValue = birthdate.value;
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const cityValue = city.value.trim();
        const districtValue = district.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (nameValue === '') {
            setError(name, 'يرجى إدخال الاسم الرباعي');
            isValid = false;
        } else {
            removeError(name);
        }

        if (!nationality || nationality === "اختر...") {
            setError(nationalityDropdownButton, "يرجى اختيار الجنسية");
            isValid = false;
        } else {
            removeError(nationalityDropdownButton);
        }

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم الهوية');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم هوية صحيح');
        } else {
            removeError(idNumber);
        }

        if (!birthdateValue) {
            setError(birthdate, "يرجى تحديد تاريخ الميلاد");
            isValid = false;
        } else {
            removeError(birthdate);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (cityValue === '') {
            setError(city, 'يرجى إدخال المدينة');
            isValid = false;
        } else {
            removeError(city);
        }

        if (districtValue === '') {
            setError(district, 'يرجى إدخال الحي');
            isValid = false;
        } else {
            removeError(district);
        }

        if (!currentPrison || currentPrison === "اختر...") {
            setError(currentPrisonDropdownButton, "يرجى اختيار السجن");
            isValid = false;
        } else {
            removeError(currentPrisonDropdownButton);
        }

        if (!destinationPrison || destinationPrison === "اختر...") {
            setError(destinationPrisonDropdownButton, "يرجى اختيار سبب طلب الخروج المؤقت");
            isValid = false;
        } else {
            removeError(destinationPrisonDropdownButton);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            const formData = new FormData(step2Form);
            formData.append('nationality', nationality);
            formData.append('currentPrison', currentPrison);
            formData.append('destinationPrison', destinationPrison);

            fetch(step2Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/PRTF/StepThree';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

// Shared PR Service forms
if (document.getElementById("SharedPR-step1-form")) {
    const step1Form = document.getElementById('SharedPR-step1-form');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');

    const dropdownButton = document.getElementById("dropdown-button");
    const dropdown = document.getElementById("select-dropdown");
    const selectedValueSpan = dropdownButton.querySelector(".selected-value");
    let applicantRole = "";

    dropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            applicantRole = option.textContent.trim();
            selectedValueSpan.textContent = applicantRole;
            removeError(dropdownButton);
        });
    });

    step1Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        let isValid = true;

        if (!applicantRole || applicantRole === "اختر...") {
            setError(dropdownButton, "يرجى اختيار صفة مقدم الطلب");
            isValid = false;
        } else {
            removeError(dropdownButton);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (isValid) {
            const formData = new FormData(step1Form);
            formData.append('applicantRole', applicantRole);

            fetch(step1Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/SharedPR/StepTwo';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("SharedPR-step2-form-a")) {
    const step2Form = document.getElementById('SharedPR-step2-form-a');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const details = document.getElementById('details');

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            window.location.href = '/SharedPR/StepThree';
        }
    });
}

if (document.getElementById("SharedPR-step2-form")) {
    const step2Form = document.getElementById('SharedPR-step2-form');
    const name = document.getElementById('name');
    const idNumber = document.getElementById('idNumber');
    const birthdate = document.getElementById('birthdate');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const city = document.getElementById('city');
    const district = document.getElementById('district');
    const details = document.getElementById('details');

    const nationalityDropdownButton = document.getElementById("nationality-dropdown-button");
    const nationalityDropdown = document.getElementById("nationality-select-dropdown");
    const nationalitySelectedValueSpan = nationalityDropdownButton.querySelector(".selected-value");
    let nationality = "";

    nationalityDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            nationality = option.textContent.trim();
            nationalitySelectedValueSpan.textContent = nationality;
            removeError(nationalityDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const nameValue = name.value.trim();
        const idNumberValue = idNumber.value.trim();
        const birthdateValue = birthdate.value;
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();
        const cityValue = city.value.trim();
        const districtValue = district.value.trim();
        const detailsValue = details.value.trim();
        let isValid = true;

        if (nameValue === '') {
            setError(name, 'يرجى إدخال الاسم الرباعي');
            isValid = false;
        } else {
            removeError(name);
        }

        if (!nationality || nationality === "اختر...") {
            setError(nationalityDropdownButton, "يرجى اختيار الجنسية");
            isValid = false;
        } else {
            removeError(nationalityDropdownButton);
        }

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم الهوية');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم هوية صحيح');
        } else {
            removeError(idNumber);
        }

        if (!birthdateValue) {
            setError(birthdate, "يرجى تحديد تاريخ الميلاد");
            isValid = false;
        } else {
            removeError(birthdate);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (cityValue === '') {
            setError(city, 'يرجى إدخال المدينة');
            isValid = false;
        } else {
            removeError(city);
        }

        if (districtValue === '') {
            setError(district, 'يرجى إدخال الحي');
            isValid = false;
        } else {
            removeError(district);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            window.location.href = '/SharedPR/StepThree';
        }
    });
}

// ES Service forms
if (document.getElementById("ES-step1-form")) {
    const step1Form = document.getElementById('ES-step1-form');

    const dropdownButton = document.getElementById("dropdown-button");
    const dropdown = document.getElementById("select-dropdown");
    const selectedValueSpan = dropdownButton.querySelector(".selected-value");
    let ESType = "";

    dropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            ESType = option.textContent.trim();
            selectedValueSpan.textContent = ESType;
            removeError(dropdownButton);
        });
    });

    step1Form.addEventListener('submit', e => {
        e.preventDefault();

        let isValid = true;

        if (!ESType || ESType === "اختر...") {
            setError(dropdownButton, "يرجى اختيار نوع الاستدعاء الإلكتروني");
            isValid = false;
        } else {
            removeError(dropdownButton);
        }

        if (isValid) {
            const formData = new FormData(step1Form);
            formData.append('ESType', ESType);

            fetch(step1Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/ES/StepTwo';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("ES-step2-form-a")) {
    const step2Form = document.getElementById('ES-step2-form-a');
    const name = document.getElementById('name');
    const idNumber = document.getElementById('idNumber');
    const birthdate = document.getElementById('birthdate');
    const phone1 = document.getElementById('phone1');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const city = document.getElementById('city');
    const district = document.getElementById('district');
    const details = document.getElementById('details');

    const nationalityDropdownButton = document.getElementById("nationality-dropdown-button");
    const nationalityDropdown = document.getElementById("nationality-select-dropdown");
    const nationalitySelectedValueSpan = nationalityDropdownButton.querySelector(".selected-value");
    let nationality = "";

    nationalityDropdown.querySelectorAll("li").forEach(option => {
        option.addEventListener("click", () => {
            nationality = option.textContent.trim();
            nationalitySelectedValueSpan.textContent = nationality;
            removeError(nationalityDropdownButton);
        });
    });

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const detailsValue = details.value.trim();
        const phoneValue1 = phone1.value.trim();
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();

        let isValid = true;

        if (phoneValue1 === '') {
            setError(phone1, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue1)) {
            setError(phone1, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue1.length < 10 || phoneValue1.length > 10) {
            setError(phone1, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        const nameValue = name.value.trim();

        if (nameValue === '') {
            setError(name, 'يرجى إدخال الاسم الرباعي');
            isValid = false;
        } else {
            removeError(name);
        }

        if (!nationality || nationality === "اختر...") {
            setError(nationalityDropdownButton, "يرجى اختيار الجنسية");
            isValid = false;
        } else {
            removeError(nationalityDropdownButton);
        }

        const idNumberValue = idNumber.value.trim();

        if (idNumberValue === '') {
            setError(idNumber, 'يرجى إدخال رقم الهوية');
        } else if (!/^\d*$/.test(idNumberValue)) {
            setError(idNumber, 'يرجى إدخال أرقام فقط');
        } else if (idNumberValue.length < 10 || idNumberValue.length > 10) {
            setError(idNumber, 'يرجى إدخال رقم هوية صحيح');
        } else {
            removeError(idNumber);
        }

        const birthdateValue = birthdate.value;

        if (!birthdateValue) {
            setError(birthdate, "يرجى تحديد تاريخ الميلاد");
            isValid = false;
        } else {
            removeError(birthdate);
        }

        const cityValue = city.value.trim();

        if (cityValue === '') {
            setError(city, 'يرجى إدخال المدينة');
            isValid = false;
        } else {
            removeError(city);
        }

        const districtValue = district.value.trim();

        if (districtValue === '') {
            setError(district, 'يرجى إدخال الحي');
            isValid = false;
        } else {
            removeError(district);
        }

        if (isValid) {
            const formData = new FormData(step2Form);
            formData.append('nationality', nationality);

            fetch(step2Form.action, {
                method: 'POST',
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.href = '/ES/StepThree';
                }
            }).catch(error => {
                console.error("Error in submitting the form:", error);
            });
        }
    });
}

if (document.getElementById("ES-step2-form")) {
    const step2Form = document.getElementById('ES-step2-form');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const details = document.getElementById('details');

    step2Form.addEventListener('submit', e => {
        e.preventDefault();

        const detailsValue = details.value.trim();
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
        } else {
            removeError(email);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
        } else {
            removeError(details);
            window.location.href = '/ES/StepThree';
        }
    });
}

// MS Service forms
if (document.getElementById("MS-step1-form")) {
    const step1Form = document.getElementById('MS-step1-form');
    const phone = document.getElementById('phone');
    const email = document.getElementById('email');
    const details = document.getElementById('details');

    step1Form.addEventListener('submit', e => {
        e.preventDefault();

        const detailsValue = details.value.trim();
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();

        let isValid = true;

        if (phoneValue === '') {
            setError(phone, 'يرجى إدخال رقم الجوال');
            isValid = false;
        } else if (!/^\d*$/.test(phoneValue)) {
            setError(phone, 'يرجى إدخال أرقام فقط');
            isValid = false;
        } else if (phoneValue.length < 10 || phoneValue.length > 10) {
            setError(phone, 'يرجى إدخال رقم جوال صحيح');
            isValid = false;
        } else {
            removeError(phone);
        }

        if (emailValue === '') {
            removeError(email);
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue)) {
            setError(email, 'يرجى إدخال بريد إلكتروني صحيح');
            isValid = false;
        } else {
            removeError(email);
        }

        if (detailsValue === '') {
            setError(details, 'يرجى إيضاح تفاصيل الطلب');
            isValid = false;
        } else if (detailsValue.length < 30) {
            setError(details, 'يجب ألا يقل عن 30 حرف');
            isValid = false;
        } else {
            removeError(details);
        }

        if (isValid) {
            window.location.href = '/MS/StepTwo';
        }
    });
}

// ------------------------------- Custom dropdown -------------------------------
document.addEventListener("DOMContentLoaded", () => {
    const customSelects = document.querySelectorAll(".custom-select");

    customSelects.forEach((customSelect) => {
        const selectButton = customSelect.querySelector(".select-button");
        const dropdown = customSelect.querySelector(".select-dropdown");
        const options = dropdown.querySelectorAll("li");
        const selectedValue = selectButton.querySelector(".selected-value");

        const requestStatusNotes = document.getElementById("requestStatusNotes");

        let focusedIndex = -1;

        const toggleDropdown = (expand = null) => {
            const isOpen =
                expand !== null ? expand : dropdown.classList.contains("hidden");
            dropdown.classList.toggle("hidden", !isOpen);
            selectButton.setAttribute("aria-expanded", isOpen);

            if (isOpen) {
                focusedIndex = [...options].findIndex((option) =>
                    option.classList.contains("selected")
                );
                focusedIndex = focusedIndex === -1 ? 0 : focusedIndex;
            } else {
                focusedIndex = -1;
            }
        };
       
        const handleOptionSelect = (option) => {
            options.forEach((opt) => opt.classList.remove("selected"));
            option.classList.add("selected");
            selectedValue.textContent = option.textContent.trim();

            if (requestStatusNotes) {
                if (selectedValue.textContent == 'معتمد') {
                    requestStatusNotes.childNodes[1].textContent = 'رقم المعاملة';
                }else if (selectedValue.textContent == 'مرفوض') {
                    requestStatusNotes.childNodes[1].textContent = 'سبب الرفض';
                }else if (selectedValue.textContent == 'غير مختص') {
                    requestStatusNotes.childNodes[1].textContent = 'جهة الاختصاص المقترحة';
                }else {
                    requestStatusNotes.childNodes[1].textContent = 'ملاحظات';
                }
            }
        };

        options.forEach((option) => {
            option.addEventListener("click", (event) => {
                event.preventDefault();
                handleOptionSelect(option);
                toggleDropdown(false);
            });
        });

        selectButton.addEventListener("click", (event) => {
            event.preventDefault();
            toggleDropdown();
        });

        selectButton.addEventListener("keydown", (event) => {
            if (event.key === "ArrowDown") {
                event.preventDefault();
                toggleDropdown(true);
            } else if (event.key === "Escape") {
                toggleDropdown(false);
            }
        });

        dropdown.addEventListener("keydown", (event) => {
            if (event.key === "ArrowDown") {
                event.preventDefault();
                focusedIndex = (focusedIndex + 1) % options.length;
            } else if (event.key === "ArrowUp") {
                event.preventDefault();
                focusedIndex = (focusedIndex - 1 + options.length) % options.length;
            }
        });

        document.addEventListener("click", (event) => {
            const isOutsideClick = !customSelect.contains(event.target);

            if (isOutsideClick) {
                toggleDropdown(false);
            }
        });
    });
});

// ------------------------------- File upload -------------------------------
if (document.querySelector(".pc-file-uploader")) {
    const dropArea = document.querySelector(".pc-file-uploader");
    const fileUploadBtn = dropArea.querySelector("button");
    const fileUploadInput = dropArea.querySelector("input");
    const dragText = dropArea.querySelector("h4");
    const uploadedFilesContainer = document.querySelector(".pc-uploaded-files");
    const sendFilesandNextBtn = document.getElementById("sendFilesandNextBtn");
    const sendFilesBtn = document.getElementById("sendFilesBtn");
    const saveRequestChangesBtn = document.getElementById("saveRequestChangesBtn");
    const attachmentsAlert = document.getElementById("attachmentsAlert");

    let files = [];

    fileUploadInput.setAttribute("multiple", "true");

    if (sendFilesandNextBtn) {
        sendFilesandNextBtn.disabled = true;
    }

    if (sendFilesBtn) {
        sendFilesBtn.disabled = true;
    }

    if (saveRequestChangesBtn) {
        saveRequestChangesBtn.disabled = true;
    }

    fileUploadBtn.onclick = () => {
        fileUploadInput.click();
    }

    fileUploadInput.addEventListener("change", function () {
        addFiles([...this.files]);
    });

    dropArea.addEventListener("dragover", (e) => {
        e.preventDefault();
        dropArea.classList.add("active");
        fileUploadBtn.disabled = true;
        dragText.textContent = "أفلت الملف هنا للإرفاق";
    });

    dropArea.addEventListener("dragleave", (e) => {
        if (!dropArea.contains(e.relatedTarget)) {
            dropArea.classList.remove("active");
            fileUploadBtn.disabled = false;
            dragText.textContent = "اسحب وأفلت الملفات هنا للإرفاق";
        }
    });

    dropArea.addEventListener("drop", (e) => {
        e.preventDefault();
        dropArea.classList.remove("active");
        fileUploadBtn.disabled = false;
        dragText.textContent = "اسحب وأفلت الملفات هنا للإرفاق";
        addFiles([...e.dataTransfer.files]);
    });

    // Prevent file from being opened when dropped outside the drop area
    ["dragover", "drop"].forEach(event => {
        document.addEventListener(event, (e) => {
            e.preventDefault();
            e.stopPropagation();
        });

        window.addEventListener(event, (e) => {
            e.preventDefault();
            e.stopPropagation();
        });
    });

    function addFiles(selectedFiles) {
        const validExtensions = ["image/png", "image/jpg", "image/jpeg", "application/pdf"];
        let maxSize = 2 * 1024 * 1024;

        selectedFiles.forEach(file => {
            let validExtension = validExtensions.includes(file.type);
            let validSize = file.size <= maxSize;

            if (validExtension && validSize) {
                files.push(file);
            }

            displayFile(file, validExtension, validSize);
        });

        updateButtonState();
    }

    function displayFile(file, validExtension, validSize) {
        const fileRow = document.createElement("div");
        fileRow.classList.add("pc-file-row");

        const fileInfo = document.createElement("div");
        fileInfo.classList.add("file-info");

        const rightContainer = document.createElement("div");
        rightContainer.classList.add("file-right");

        const fileIcon = document.createElement("img");
        const fileName = document.createElement("p");
        fileName.classList.add("file-name");
        fileName.textContent = file.name;

        const deleteIcon = document.createElement("img");
        deleteIcon.src = "/icons/xIcon.svg";
        deleteIcon.width = 12;
        deleteIcon.height = 12;
        deleteIcon.classList.add("delete-file");

        const helperText = document.createElement("p");
        helperText.classList.add("helper-text");

        if (!validExtension) {
            fileIcon.src = "/icons/infoFilled.svg";
            fileRow.style.borderColor = "#B42318";
            helperText.textContent = "الرجاء تحميل ملف بصيغة مدعومة: .jpg، .png، .pdf";
        } else if (!validSize) {
            fileIcon.src = "/icons/infoFilled.svg";
            fileRow.style.borderColor = "#B42318";
            helperText.textContent = " حجم الملف يجب أن يكون 2 ميجابايت أو أقل";
        } else {
            fileIcon.src = "/icons/checkmarkFilled.svg";
            helperText.textContent = "";
            helperText.style.display = "none";
        }

        fileIcon.width = 20;
        fileIcon.height = 20;

        deleteIcon.onclick = () => {
            files = files.filter(f => f !== file);
            fileRow.remove();
            updateButtonState();
        };

        rightContainer.appendChild(fileIcon);
        rightContainer.appendChild(fileName);

        fileInfo.appendChild(rightContainer);
        fileInfo.appendChild(deleteIcon);

        fileRow.appendChild(fileInfo);
        fileRow.appendChild(helperText);

        uploadedFilesContainer.appendChild(fileRow);

        if (!validExtension || !validSize) {
            setTimeout(() => {
                fileRow.remove();
                updateButtonState();
            }, 10000);
        }
    }

    //backend: save the uploaded files to the db
    function updateButtonState() {
        if (sendFilesandNextBtn) {
            sendFilesandNextBtn.disabled = files.length === 0;
        }

        if (sendFilesBtn) {
            sendFilesBtn.disabled = files.length === 0;

            sendFilesBtn.addEventListener("click", () => {
                attachmentsAlert.style.display = "flex";
                saveRequestChangesBtn.disabled = false;
            });
        }
    }

    if (sendFilesandNextBtn) {
        sendFilesandNextBtn.addEventListener("click", e => {
            e.preventDefault();

            const currentUrl = window.location.pathname;
            const segments = currentUrl.split("/");
            const routeId = segments.length > 2 ? segments[1] : null;

            if (routeId) {
                window.location.href = `/${routeId}/ReviewRequest`;
            } else {
                window.location.href = `/ReviewRequest`;
            }
        });
    }
}

// ------------------------------- Request status -------------------------------
if (document.querySelector(".request-status-container")) {
    function createRequestStatusComponent(message) {
        const requestStatus = document.createElement("div");
        requestStatus.classList.add("request-status");

        const img = document.createElement("img");
        img.src = "../icons/progressIndicator.svg";
        img.alt = "Progress Indicator";
        img.width = 32;
        img.height = 128;

        const textContainer = document.createElement("div");

        const statusText = document.createElement("p");
        statusText.classList.add("request-progress-data");
        statusText.textContent = message;

        const statusDate = document.createElement("p");
        statusDate.classList.add("request-progress-date");
        statusDate.textContent = new Date().toLocaleString("en-CA", {
            year: "numeric",
            month: "numeric",
            day: "numeric",
            hour: "2-digit",
            minute: "2-digit",
            hour12: false
        }).replace(",", "");

        textContainer.appendChild(statusText);
        textContainer.appendChild(statusDate);

        requestStatus.appendChild(img);
        requestStatus.appendChild(textContainer);
        
        return requestStatus;
    }

    const saveRequestChangesBtn = document.getElementById("saveRequestChangesBtn");
    const requestStatusTag = document.getElementById("requestStatusTag");
    const changesSavedAlert = document.getElementById("changesSavedAlert");

    if (saveRequestChangesBtn) {
        saveRequestChangesBtn.addEventListener("click", () => {
            changesSavedAlert.style.display = "flex";
            setTimeout(() => {
                changesSavedAlert.style.display = "none";
            }, 10000);

            attachmentsAlert.style.display = "none";
            saveRequestChangesBtn.style.display = "none";
            completeRequestBtn.style.display = "none";

            requestStatusTag.classList.remove("pc-orange-status");
            requestStatusTag.classList.add("pc-blue-status");
            requestStatusTag.textContent = "تحت المراجعة";

            const requestStatusBody = document.querySelector(".request-status-body");
            if (requestStatusBody) {
                requestStatusBody.appendChild(createRequestStatusComponent("تم تحديث مرفقات الطلب"));
                requestStatusBody.appendChild(createRequestStatusComponent("تم تحديث حالة الطلب إلى ”تحت المراجعة“"));
            }
        });
    }

    // copy transaction number if request status = approved
    const copyTransactionNumBtn = document.getElementById("copyTransactionNumBtn");
    const copyTransactionNumAlert = document.getElementById("copyTransactionNumAlert");

    if (copyTransactionNumBtn) {
        copyTransactionNumBtn.addEventListener("click", function () {
            const numberToCopy = document.getElementById("transactionNum").textContent;

            navigator.clipboard.writeText(numberToCopy).then(() => {
                copyTransactionNumAlert.style.display = "flex";
                setTimeout(() => {
                    copyTransactionNumAlert.style.display = "none";
                }, 10000);
            }).catch(err => {
                console.error("فشل النسخ: ", err);
            });
        });
    }
}

// ------------------------------- Print page -------------------------------
document.addEventListener("DOMContentLoaded", () => {
    const printButton = document.getElementById("printButton");
    if (printButton) {
        printButton.addEventListener("click", () => {
            window.print();
        });
    }
});

// ------------------------------- Filter and search cards -------------------------------
if (document.querySelector(".pc-tabs-search")) {
    const cards = document.querySelectorAll(".pc-card");

    // Filter cards based on the filters
    const tabs = document.querySelectorAll(".pc-tab");

    tabs.forEach(tab => {
        tab.addEventListener("click", () => {
            const filter = tab.getAttribute("data-filter");

            cards.forEach(card => {
                if (filter === "all" || card.getAttribute("data-category") === filter) {
                    card.style.display = "flex";
                } else {
                    card.style.display = "none";
                }
            });

        });
    });

    // Filter cards based on the search input
    const searchInput = document.querySelector(".pc-search-input");
    const clearSearchBtn = document.querySelector(".pc-clear-search-btn");
    const clearSearchFieldBtn = document.getElementById("clear-search-field-btn");
    const emptyState = document.querySelector(".pc-empty-state");

    searchInput.addEventListener("input", () => {
        const query = searchInput.value.trim().toLowerCase();
        clearSearchBtn.style.display = query.length > 0 ? "flex" : "none";

        let visibleCount = 0;

        cards.forEach(card => {
            const cardText = card.innerText.toLowerCase();

            if (query.length === 0) {
                clearSearchBtn.style.display = "none";
                card.style.display = "flex";
                visibleCount++;
            }
            else if (cardText.includes(query)) {
                card.style.display = "flex";
                visibleCount++;
            } else {
                card.style.display = "none";
            }
        });

        emptyState.style.display = visibleCount > 0 ? "none" : "flex";
    });

    const clearSearch = () => {
        searchInput.value = "";
        cards.forEach(card => {
            card.style.display = "flex";
        });
        clearSearchBtn.style.display = "none";
        emptyState.style.display = "none";
    };

    clearSearchBtn.addEventListener("click", clearSearch);
    clearSearchFieldBtn.addEventListener("click", clearSearch);

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

// ------------------------------- Requests search -------------------------------
if (document.getElementById("requestsSearch")) {
    const tableRows = document.querySelectorAll("#requestsTableBody tr");

    const searchInput = document.querySelector(".pc-search-input");
    const clearSearchBtn = document.querySelector(".pc-clear-search-btn");
    const clearSearchFieldBtn = document.getElementById("clear-search-field-btn");
    const emptyState = document.querySelector(".pc-empty-state");

    const pagination = document.getElementById("pagination-controls");

    searchInput.addEventListener("input", () => {
        const query = searchInput.value.trim().toLowerCase();
        clearSearchBtn.style.display = query.length > 0 ? "flex" : "none";

        let visibleCount = 0;

        tableRows.forEach(row => {
            const requestNumber = row.children[2].innerText.trim().toLowerCase();
            const applicantName = row.children[3].innerText.trim().toLowerCase();

            if (query.length === 0 || requestNumber.includes(query) || applicantName.includes(query)) {
                row.style.display = "table-row";
                pagination.style.display = "none";
                visibleCount++;
            } else {
                row.style.display = "none";
            }
        });

        emptyState.style.display = visibleCount > 0 ? "none" : "flex";

        if (query.length === 0) {
            pagination.style.display = visibleCount > 0 ? "flex" : "none";
        }
    });

    const clearSearch = () => {
        searchInput.value = "";

        tableRows.forEach(row => {
            row.style.display = "table-row";
        });

        clearSearchBtn.style.display = "none";
        emptyState.style.display = "none";
        pagination.style.display = "flex";
    };

    clearSearchBtn.addEventListener("click", clearSearch);
    clearSearchFieldBtn.addEventListener("click", clearSearch);
}

// ------------------------------- All Requests Table -------------------------------
if (document.getElementById("allRequestsTable")) {
    const rowsPerPage = 10;
    let currentPage = 1;
    const tableBody = document.getElementById("requestsTableBody");
    const rows = tableBody.getElementsByTagName("tr");
    const totalRows = rows.length;
    const totalPages = Math.ceil(totalRows / rowsPerPage);
    const pageNumbersContainer = document.getElementById("pageNumbers");

    function showPage(page) {
        let start = (page - 1) * rowsPerPage;
        let end = start + rowsPerPage;

        for (let i = 0; i < totalRows; i++) {
            rows[i].style.display = (i >= start && i < end) ? "table-row" : "none";
        }

        document.getElementById("prevPage").disabled = page === 1;
        document.getElementById("nextPage").disabled = page === totalPages;
        updatePaginationNumbers();

        document.getElementById("allRequestsTable1").scrollIntoView({ behavior: "smooth", block: "start" });
    }

    function updatePaginationNumbers() {
        pageNumbersContainer.innerHTML = "";
        let maxPagesToShow = 5;
        let startPage, endPage;

        if (totalPages <= maxPagesToShow) {
            startPage = 1;
            endPage = totalPages;
        } else if (currentPage <= 3) {
            startPage = 1;
            endPage = maxPagesToShow;
        } else if (currentPage >= totalPages - 2) {
            startPage = totalPages - 4;
            endPage = totalPages;
        } else {
            startPage = currentPage - 2;
            endPage = currentPage + 2;
        }

        if (startPage > 1) {
            addPageButton(1);
            if (startPage > 2) {
                addEllipsis();
            }
        }

        for (let i = startPage; i <= endPage; i++) {
            addPageButton(i);
        }

        if (endPage < totalPages) {
            if (endPage < totalPages - 1) {
                addEllipsis();
            }
            addPageButton(totalPages);
        }
    }

    function addPageButton(pageNumber) {
        let pageBtn = document.createElement("button");
        pageBtn.textContent = pageNumber;
        pageBtn.classList.add("pagination-button");
        if (pageNumber === currentPage) {
            pageBtn.classList.add("active");
        }
        pageBtn.addEventListener("click", function () {
            currentPage = pageNumber;
            showPage(currentPage);
        });
        pageNumbersContainer.appendChild(pageBtn);
    }

    function addEllipsis() {
        let ellipsis = document.createElement("span");
        ellipsis.textContent = "...";
        ellipsis.classList.add("ellipsis");
        pageNumbersContainer.appendChild(ellipsis);
    }

    document.getElementById("prevPage").addEventListener("click", function () {
        if (currentPage > 1) {
            currentPage--;
            showPage(currentPage);
        }
    });

    document.getElementById("nextPage").addEventListener("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            showPage(currentPage);
        }
    });

        showPage(currentPage);
}

// ------------------------------- Open file -------------------------------
function openFile(fileName) {
    var filePath = '/reports/' + fileName;
    window.open(filePath, '_blanck');
}

// ------------------------------- Edit request -------------------------------
document.addEventListener("DOMContentLoaded", () => {
    const editRequestBtn = document.getElementById("editRequestBtn");
    const cancelEditingRequestBtn = document.getElementById("cancelEditingRequestBtn");
    const requestStatusContainer = document.getElementById("requestStatusContainer");
    const editRequestContainer = document.getElementById("editRequestContainer");

    if (editRequestBtn) {
        editRequestBtn.addEventListener("click", (e) => {
            e.preventDefault();
            requestStatusContainer.style.display = "none";
            editRequestContainer.style.display = "flex";
        });
    }

    if (cancelEditingRequestBtn) {
        cancelEditingRequestBtn.addEventListener("click", (e) => {
            e.preventDefault();
            requestStatusContainer.style.display = "flex";
            editRequestContainer.style.display = "none";
        });
    }
});

let sessionTimeout = 20 * 60 * 1000;
let warningTime = 1 * 60 * 1000;
let alertTimer, countdownTimer;

function startTimers() {
    alertTimer = setTimeout(() => {
        showSessionModal();
    }, sessionTimeout - warningTime);
}

function showSessionModal() {
    const myModal = new bootstrap.Modal(document.getElementById('sessionModal'));
    myModal.show();

    let timeLeft = warningTime / 1000;
    const display = document.getElementById('sessionTimer');
    const unitDisplay = document.getElementById('timerUnit');

    function updateTimerDisplay() {
        if (timeLeft >= 60) {
            let minutes = Math.floor(timeLeft / 60);
            let seconds = timeLeft % 60;
            display.innerText = `${minutes < 10 ? "0" : ""}${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;
            unitDisplay.innerText = "minutos";
        } else {
            display.innerText = timeLeft;
            unitDisplay.innerText = "segundos";
        }
    }

    updateTimerDisplay();

    countdownTimer = setInterval(() => {
        timeLeft--;

        if (timeLeft <= 0) {
            clearInterval(countdownTimer);
            display.innerText = "0";
            executeLogout();
            return;
        }

        updateTimerDisplay();
    }, 1000);
}

function executeLogout() {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = '/Auth/Logout';

    const input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'isAutomatic';
    input.value = 'true';

    form.appendChild(input);
    document.body.appendChild(form);
    form.submit();

    setTimeout(() => {
        window.location.href = '/Auth/Logout';
    }, 2000);
}

function extendSession() {
    fetch('/Home/KeepAlive')
        .then(() => {
            const modalElement = document.getElementById('sessionModal');
            const instance = bootstrap.Modal.getInstance(modalElement);
            instance.hide();
            clearInterval(countdownTimer);
            clearTimeout(alertTimer);
            startTimers();
        });
}
window.blazorHelpers = {
    getFromLocalStorage: function (key) {
        return localStorage.getItem(key);
    },
    saveToLocalStorage: function (key, value) {
        localStorage.setItem(key, value);
    },
    removeFromLocalStorage: function (key) {
        localStorage.removeItem(key);
    }
};
window.logToConsole = (item) => {
    console.log(item);
};
window.ShowAlert = (item) => {
    alert(item);
}
window.triggerClick = (elt) => elt.click();

window.Toast = Swal.mixin({
    toast: true,
    position: "bottom",
    iconColor: 'white',
    customClass: {
        popup: 'colored-toast',
    },
    showConfirmButton: false,
    timer: 2000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    }
});
function confirmDelete() {
    return confirm('Emin misiniz?');
}
function showDeleteConfirmation() {
    return Swal.fire({
        title: 'Silmek istediğinize emin misiniz?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sil',
        denyButtonText: `Vazgeç`,
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire('Silindi!', '', 'success');
            return true;
        } else {
            return false;
        }
    });
}
async function AlertToggle(message) {

    var popupElements = document.querySelectorAll('[id="popup"]');
    var i;
    for (i = 0; i < popupElements.length; i++) {
        var popupElement = popupElements[i];
        var close = popupElement.firstChild;
        close.onclick = function () {
           
            popupElement.style.opacity = "0";
            setTimeout(function () { popupElement.style.display = "none"; }, 200);
        }
       
        if (popupElement.style.display == "none") {
           
            popupElement.style.opacity = "1";
            setTimeout(function () { popupElement.style.display = ""; }, 200);
        }
        else {
           
            popupElement.style.opacity = "0";
            setTimeout(function () { popupElement.style.display = "none"; }, 200);
        }

    }
    console.log(message);
}
    async function ShowPopUp(id) {
    
        var popupElement = document.getElementById(id);       
        popupElement.style.opacity = 1;
        popupElement.style.display = "block";
        console.log(popupElement);
        setTimeout(function () {
            popupElement.style.opacity = 0;
            popupElement.style.display = "none";
        }, 3000);
    }
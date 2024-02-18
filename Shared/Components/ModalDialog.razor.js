window.showModal = (id) => {
    let el = document.getElementById(id);
    el.style.display = "block";
}

window.hideModal = (id) => {
    let el = document.getElementById(id);
    el.style.display = "none";
}
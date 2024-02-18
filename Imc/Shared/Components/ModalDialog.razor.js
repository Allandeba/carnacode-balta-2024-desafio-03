window.showModal = (id) => {
    let el = document.getElementById(id);
    el.showModal();
}

window.hideModal = (id) => {
    let el = document.getElementById(id);
    console.log(el);
    el.remove();
    console.log(el);
}
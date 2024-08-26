
//modal de confirmación

const btnEliminarTodos = document.querySelectorAll('.btnEliminar');

for (let i = 0; i < btnEliminarTodos.length; i++) {
    btnEliminarTodos[i].addEventListener('click', (event) => {
        event.preventDefault();
        const icon = event.target;
        const modalConfirmacion = icon.parentElement.parentElement.nextElementSibling;

        modalConfirmacion.showModal();

        const btnCerrar = modalConfirmacion.querySelector('.btnCerrarModal');
        btnCerrar.addEventListener('click', () => modalConfirmacion.close());
    });
}
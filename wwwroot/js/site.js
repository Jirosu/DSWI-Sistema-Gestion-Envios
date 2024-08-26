const detalleIcons = document.querySelectorAll('.icon-detalle');
const actualizarIcons = document.querySelectorAll('.icon-actualizar');
const eliminarIcons = document.querySelectorAll('.icon-eliminar');



for (const link of detalleIcons) {
    link.innerHTML = `
  <div class="icon icon-detalle">
    <i class="bi bi-info-circle "></i>
  </div>`;
}

for (const link of actualizarIcons) {
    link.innerHTML = `
  <div class="icon icon-actualizar">
    <i class="bi bi-pencil"></i>
  </div>`;
}

for (const link of eliminarIcons) {
    link.innerHTML = `
  <div class="icon icon-eliminar">
    <i class="bi bi-x-circle"></i>
  </div>`;
}


//modal de confirmación

const btnEliminarTodos = document.querySelectorAll('.btnEliminar');

for (let i = 0; i < btnEliminarTodos.length; i++) {
    btnEliminarTodos[i].addEventListener('click', (event) => {
        event.preventDefault();
        const icon = event.target;
        let modalConfirmacion;
        
        modalConfirmacion = icon.parentElement.parentElement.nextElementSibling;

        if (modalConfirmacion == null) {
            modalConfirmacion = icon.nextElementSibling;
        }

        modalConfirmacion.showModal();

        const btnCerrar = modalConfirmacion.querySelector('.btnCerrarModal');
        btnCerrar.addEventListener('click', () => modalConfirmacion.close());        
    });
}


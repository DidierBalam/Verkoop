/*Se selecciona div con el id ContenedorDirección, al obtener el div, al hacer clic en btnEliminar se obtiene el id de la dirección que se guarda en Dirección.*/
$("#ContenedorDireccion").on("click ", ".btnEliminar", function () {
    let Direccion = $(this).attr('idDireccion');
});


function ELiminarDireccion() {

}
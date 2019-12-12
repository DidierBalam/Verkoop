$(document).ready(function () {
    IniciarBotonDireccion()
})

function IniciarBotonDireccion() {
    $("#btnAgregarDireccion").click(function (e) {
        e.preventDefault();
        ObtenerVista("POST", "..//")
    })


    /*Se selecciona div con el id ContenedorDirección, al obtener el div, al hacer clic en btnEliminar se obtiene el id de la dirección que se guarda en Dirección.*/

    $(".btnEliminarDireccion").click(function (e) {
        e.preventDefault();
        EliminarDireccion($(this).attr("idDireccion"), $(this).parentsUntil(".-cardPadre"));
    });
}



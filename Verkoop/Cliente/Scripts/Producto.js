$(document).ready(function () {
    Barraherramientas();
});

function Barraherramientas() {
    $("#btnVer").click(function (e) {
        e.preventDefault();
        ObtenerVistaDetalles("POST", "../Producto/DetallesProductos", null);///Se le agrega la función Modal al botón de guardar del modal.
    });

    $("#cVerificarContrasenia").click(function (e) {
        console.log(".")        
        window.location.href = "../Producto/Principal"
    })
}








$(document).ready(function () {
    IniciarBoton()
});

function IniciarBoton() {

    $("#btnAgregarTarjeta").click(function (e) {
        e.preventDefault();
        ObtenerVista("POST", "../Perfil/NuevaTarjeta");
        
    });

}
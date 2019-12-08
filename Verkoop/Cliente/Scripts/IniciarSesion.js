$(document).ready(function () {
    Boton();
});

function Boton() {
    $("#btnIniciarSesion").click(function (e) {

        e.preventDefault();

        IniciarSesion($("#cCorreo").val(), $("#cContrasenia").val());

    });
}


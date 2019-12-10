
/*Sirve para abrir una vista del tab de registro.*/
$('#cContinuar').click(function (e) {
    e.preventDefault();    
    $('#myTab a[href="#cContrasenia"]').tab('show');
})

/*Sirve para abrir una vista del tab de registro.*/
$('#cAtras').click(function (e) {
    e.preventDefault();
    console.log("Si funciona")
    $('#myTab a[href="#cRegistro"]').tab('show');
})


function IniciarSesion(cCorreo, cContrasenia) {
    let Data = {};

    Data["Correo"] = JSON.stringify(cCorreo);
    Data["Contrasenia"] = JSON.stringify(cContrasenia);

    ObtenerMetodoControlador("POST", "/Sesion/IniciarSesion", Data).then((objRespuesta) => {
        alert(objRespuesta._bEstadoOperacion + ": " + objRespuesta._cMensaje);
    });
}

/**
* FUNCIÓN AJAX QUE CONECTA A LOS MÉTODOS DEL CONTROLADOR
* @param {any} cMetodo Recibe la url del método.
* @param {any} datoEnvio Recibe los datos a enviar.
*/
function ObtenerMetodoControlador(cTipo, cUrl, Data) {

    return new Promise((objResultado) => {

        $.ajax({
            url: cUrl,
            type: cTipo,
            data: Data,
            async: false,
            dataType: 'json',
            success: function (Respuesta) {

                objResultado(Respuesta);
            }
        });

    })
}
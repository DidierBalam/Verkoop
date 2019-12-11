
/*Sirve para abrir una vista del tab de registro.*/
$('#cContinuar').click(function (e) {
    e.preventDefault();
    $('#myTab a[href="#cContrasenia"]').tab('show');
})

/*Sirve para abrir una vista del tab de registro.*/
$('#cAtras').click(function (e) {
    e.preventDefault();
    $('#myTab a[href="#cRegistro"]').tab('show');
})


/**
 * Función para iniciar sesión por medio de Json
 * @param {any} cCorreo Recibe el correo electrónico
 * @param {any} cContrasenia Recibe la contraseña
 */
function IniciarSesion(cCorreo, cContrasenia) {
    let Data = {};

    Data["Correo"] = JSON.stringify(cCorreo);
    Data["Contrasenia"] = JSON.stringify(cContrasenia);

    ObtenerMetodoControlador("POST", "/Sesion/IniciarSesion", Data).then((objRespuesta) => {

        if (objRespuesta._bEstadoOperacion == true) { //se obtiene la respuesta verdadera y redirecciona a la vista principal
            llamarSwetalert(objRespuesta);
            window.location.replace("/Producto/Principal")
        }
        else {

            llamarSwetalert(objRespuesta);
        }
    });
}

function EliminarTarjeta(iIdTarjeta) {
    ObtenerMetodoControlador("POST", "/Tarjeta/EliminarTarjeta", { idTarjeta: iIdTarjeta }).then((objRespuesta) => {

    });

}



/**
 * Función para eliminar una dirección.
 * @param {any} iIdDireccion Recibe el id de la dirección.
 */
function EliminarDireccion(iIdDireccion) {
    console.log(iIdDireccion)
    ObtenerMetodoControlador("POST", "/Direccion/EliminarDireccion", { _iIdDireccion: iIdDireccion }, "JSON").then((objRespuesta) => {
        alert(objRespuesta._bEstadoOperacion + " : " + objRespuesta._cMensaje);
      

    })
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

/**
 * Función para ejecutar una alerta
 * @param {any} cTipo El icono
 * @param {any} cTitulo El titulo  
 * @param {any} cTexto El mensaje
 */
function EjecutarAlerta(cTipo, cTitulo, cTexto) {
    Swal.fire({
        position: 'center',
        icon: cTipo,
        title: cTitulo,
        text: cTexto,
        showConfirmButton: false,
        timer: 1500
    })
}

/**
 * Función para llamar el swetAlert
 * @param {any} objRespuesta recibe el objeto respuesta
 */
function llamarSwetalert(objRespuesta) {

    if (objRespuesta._bEstadoOperacion) {

        EjecutarAlerta("success", "Ok", objRespuesta._cMensaje);
    }
    else {
        EjecutarAlerta("error", "Error", objRespuesta._cMensaje);
    }
}
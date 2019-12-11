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

/**
 * Esta función Elimina tarjeta de usuario
 * @param {any} iIdTarjeta contiene el id de la tarjeta 
 * @param {any} cElemento contiene el cardPadre que será removido 
 */
function EliminarTarjeta(iIdTarjeta, cElemento) {

    ObtenerMetodoControlador("POST", "../Tarjeta/EliminarTarjeta", { _iIdTarjeta: iIdTarjeta }).then((objRespuesta) => {
        if (objRespuesta._bEstadoOperacion == true) {

            //console.log(objRespuesta);
            llamarSwetalert(objRespuesta);

            EliminarElementoHTML(cElemento);

        }
        else {

            llamarSwetalert(objRespuesta);
        }

    });

}

/**
 * función que elimina elementos HTML
 * @param {any} cElemento contiene el elemento que sera removido
 */
function EliminarElementoHTML(cElemento) {

    cElemento.remove();

}

function GuardarTarjeta(cElemento, objTarjeta) {

    let Data = {};

    Data["Tarjeta"] = JSON.stringify(objTarjeta);

    ObtenerMetodoControlador("POST", "/Tarjeta/GuardarTarjeta", Data).then((objRespuesta) => {

        if (objRespuesta._bEstadoOperacion == true) { //se obtiene la respuesta verdadera y redirecciona a la vista principal

            $('#ModalPrincipal').modal('hide');

            llamarSwetalert(objRespuesta);

            InsertarCardTarjeta(objRespuesta._objDatosTarjeta, cElemento);
        }
        else {

            llamarSwetalert(objRespuesta);
        }
    });
}

/** 
 * función que pinta el card de la tarjeta
 * @param {any} objTarjeta contiene el objeto de la tarjeta
 * @param {any} cElemento contiene el elemento del HTML 
 */
function InsertarCardTarjeta(objTarjeta, cElemento) {
    let cTarjeta = '<div class="-cardPadre">' +
        ' <div class="-cardDireccion" >' +
        '  <div class="card">' +
        ' <div class="card-body">' +
        ' <div>' +
        '<div class="-cardDireciones ">' +
        '<h6 class="-cardDireciones">' +
        'Número: <strong class="-TamCol">' +
        objTarjeta.cNumeroTarjeta +
    '</strong>' +
        '</h6>' +
        '<div >' +

        '<a idTarjeta="' + objTarjeta.iIdTarjeta + '" class="btnEliminarTarjeta">' +
        '<img class="-cardDireciones3" src="~/Assets/img/core-img/basura.svg"' +
        'alt="">' +
        '</a>' +

        '</div>' +
        '</div>' +

        '<div>' +
        '<p class="-cardDireciones4">' +
        'Mes de vigencia: <strong class="-TamCol2">' +
        objTarjeta.cMesVigencia +
        '</strong>' +
        '</p>' +
        '</div>' +

        '<div>' +
        '<p class="-cardDireciones4">' +
        'Año de vigencia: <strong class="-TamCol2">' +
        objTarjeta.cAnioVigencia +
        '</strong>' +
        '</p>' +
        '</div>' +

        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div >'; 

    $(cElemento).append(cTarjeta);
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

                console.log(Respuesta);

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
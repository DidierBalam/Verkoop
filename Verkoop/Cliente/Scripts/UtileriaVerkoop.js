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
 * FUNCIÓN PARA INICIAR SESIÓN.
 * @param {any} cCorreo Recibe el correo del usuario.
 * @param {any} cContrasenia Recibe la contraseña del usuario.
 */
function IniciarSesion(cCorreo, cContrasenia) {
    let Data = {};

    Data["Correo"] = JSON.stringify(cCorreo);
    Data["Contrasenia"] = JSON.stringify(cContrasenia);

    ObtenerMetodoControlador("POST", "/Sesion/IniciarSesion", Data, "JSON").then((objRespuesta) => {

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
    })
}

    function EliminarTarjeta(iIdTarjeta) {
        ObtenerMetodoControlador("POST", "/Tarjeta/EliminarTarjeta", { idTarjeta: iIdTarjeta }, "JSON").then((objRespuesta) => {

        });

    }

    function AgregarProductoCarrito(iIdProducto) {

        ObtenerMetodoControlador("POST", "Cliente/CarritoCompras/AgregarProductoCarrito", { _iIdProducto: iIdProducto }, "JSON").then((objRespuesta) => {
            console.log(objRespuesta)
            llamarSwetalert(objRespuesta)
        })

    }

    /**
     * Función para eliminar una dirección.
     * @param {any} iIdDirecion Recibe el id de la dirección.
     */
    function EliminarDireccion(iIdDirecion) {

        let Data = {};

        Data["iIdDireccion"] = JSON.stringify(iIdDirecion);
        ObtenerMetodoControlador("POST", "/Perfil/Direcciones", { iIdDireccion: iIDireccion }, "JSON").then((objRespuesta) => {
            alert(objRespuesta._bEstadoOperacion + " : " + objRespuesta._cMensaje);

        })
    }

    /**
     * FUNCIÓN PARA VISUALIZAR LOS DETALLES DEL PRODUCTO.
     * @param {any} iIdProducto Recibe el id del producto.
     */
    function VisualizarDetallesProducto(iIdProducto) {

        ObtenerMetodoControlador("POST", "Cliente/Producto/DetallesProductos", { iIdProducto: iIdProducto }, "HTML").then((objRespuesta) => {

            $("#ModalDetalles").html(objRespuesta);
            $("#ModalDetalles").modal({
                show: true,

            });
        });
    }

    /**
     * FUNCIÓN PARA BUSCAR PRODUCTOS POR NOMBRE.
     * @param {any} cNombre Recibe el nombre del producto.
     * @param {any} iNumeroConsulta Recibe el número de consulta realizada (Por defecto debe llegar 0).
     */
    function BuscarProducto(cNombre, iConsulta) {

        ObtenerMetodoControlador("POST", "Cliente/Producto/BuscarProducto", { _cNombre: cNombre, _iNumeroConsulta: iConsulta }, "HTML").then((objRespuesta) => {

            if (objRespuesta.trim() != "") {

                $("#contendorProductos").html(objRespuesta);

                iNumeroConsulta += 1;

            }
            else {

                let cEtiqueta = FormarEtiquetaMensajeBusqueda('No se encontraron productos');

                $("#contendorProductos").html(cEtiqueta);
            }
        });

    }

    /**
     * FUNCIÓN PARA GENERAR UN MENSAJE EN CASO DE PRODUCTOS NO ENCONTRADOS.
     * @param {any} cMensaje Recibe el mensaje.
     */
    function FormarEtiquetaMensajeBusqueda(cMensaje) {

        let cEtiqueta = "<div class='-flex-center-center -width-100pst -padding-t10pst'>" +
            "<h4> " + cMensaje + "</h4>" +
            "</div >";

        return cEtiqueta;
    }

    /**
     *  FUNCIÓN PARA VER MÁS PRODUCTOS AL LLEGAR AL FINAL DE LA PÁGINA.
     * @param {any} cFiltro Recibe el filtro deseado.
     * @param {any} iNumeroConsulta Recibe el número de consulta realizadas.
     */
    function VerMasProductos(cFiltro, iConsulta, cContenedor) {

        CargarSpiners(cContenedor);

        if (cFiltro.toUpperCase() == 'BUSCAR') {

            ObtenerMetodoControlador("POST", "Cliente/Producto/BuscarProducto", { _cNombre: $("#BarraBusqueda").val(), _iNumeroConsulta: iConsulta }, "HTML").then((objRespuesta) => {

                if (objRespuesta.trim() != "") {

                    $("#contendorProductos").append(objRespuesta);

                    iNumeroConsulta += 1;

                }
            });

        } else {

            ObtenerMetodoControlador("POST", "Cliente/Producto/VerMasProductos", { _cFiltro: cFiltro.toUpperCase(), _iNumeroConsulta: iConsulta }, "HTML").then((objRespuesta) => {

                if (objRespuesta.trim() != "") {

                    $("#contendorProductos").append(objRespuesta);

                    $(cContenedor).html = "";

                    iNumeroConsulta += 1;

                }


            });
        }



    }

    /**
     * FUNCIÓN PARA CARGAR LOS SNIPERS A UN ELEMENTO CORRESPONDIENTE.
     * @param {any} cContenedor Recibe el elemento html.
     */
    function CargarSpiners(cContenedor) {

        let cSpiners = '<div class="spinner-grow text-info" role="status">' +
            '<span class="sr-only" > Loading...</span >' +
            '</div>' +
            '<div class="spinner-grow text-info" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            '</div>' +
            '<div class="spinner-grow text-info" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            '</div>' +
            '<div class="spinner-grow text-info" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            ' </div>' +
            '<div class="spinner-grow text-info" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            '</div>' +
            '<div class="spinner-grow text-primary" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            '</div>' +
            '<div class="spinner-grow text-primary" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            ' </div>' +
            '<div class="spinner-grow text-primary" role="status">' +
            '<span class="sr-only">Loading...</span>' +
            '</div>';
        $(cContenedor).html = cSpiners;
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
    function ObtenerMetodoControlador(cTipo, cUrl, Data, cTipoDato) {

        return new Promise((objResultado) => {

            $.ajax({
                url: cUrl,
                type: cTipo,
                data: Data,
                async: false,
                dataType: cTipoDato,
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
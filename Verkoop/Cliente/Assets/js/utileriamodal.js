/**
 * OBTIENE UNA VISTA
 * @param {any} cTipo GET / POST
 * @param {any} cUrl DIRECCION DEL METODO A EJECUTAR
 * @param {any} Data
 * @para {any} function nombre de la funcion a ejecutar
 */
function ObtenerVista(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "HTML",
        success: function (response) {
            $("#ModalPrincipal").html(response);
            $("#ModalPrincipal").modal({
                show: true,
                backdrop: "static"
            });

            if (Funcion) {

                window[Funcion]();
            }
        }
    });
}

/**
 * eiecuta un método a travez del nombre
 * @param {any} cTipo get/post
 * @param {any} cUrl direccion del metod a ejecuta
 * @param {any} Data informaicon a actualizar o guardar
 * @param {any} Funcion nombre de la funcion a ejectuar
 */
function LlamarMetodo(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        data: Data,
        async: false,
        dataType: "JSON",
        success: function (response) {
            $("#ModalPrincipal").modal('hide');
            alert(response.cMensaje);
            //imprime un mensaje de la respuesta del servicio web que se este llamando 
            if (Funcion) {
                window[Funcion]();
            }

        }
    });
}
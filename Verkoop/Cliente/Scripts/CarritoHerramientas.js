$(document).ready(function () {

    leerRadioButtonPago()

});

/**
 * Función que sirve para leer el valor de un radio button seleccionado.
 * */
function leerRadioButtonPago() {

    $("#btnPagar").click(function () {


        let radioButtonSeleccionado = document.querySelector('input[name=inlineRadioOptions]:checked')

        if (radioButtonSeleccionado != null) {

            console.log(radioButtonSeleccionado.value)

        }

    })

}


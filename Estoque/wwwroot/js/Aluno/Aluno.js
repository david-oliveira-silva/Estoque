function formatarCPF(campo) {

    let valor = campo.value.replace(/\D/g, '')

    if (valor.length > 3) {
        valor = valor.substring(0, 3) + '.' + valor.substring(3);
    }

    if (valor.length > 7) {
        valor = valor.substring(0, 7) + '.' + valor.substring(7);
    }
    if (valor.length > 11) {
        valor = valor.substring(0, 11) + '-' + valor.substring(11);
    }

    campo.value = valor;
}
if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Contact) == "undefined") { Logistics.Contact = {} }

Logistics.Contact =
{
    OnChangeCPF: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var cpf = formContext.getAttribute("dcp_cpf").getValue();

        if (cpf != null && this.ValidaCpf(executionContext) != false) {

            if (cpf.length == 11) {
                var formattedCPF = cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
                formContext.getAttribute("dcp_cpf").setValue(formattedCPF);
            }
            else {
                formContext.getAttribute("dcp_cnpj").setValue(null);
            }
        }
    },
    ValidaCpf: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var cpf = formContext.getAttribute("dcp_cpf").getValue();

        var sum;
        var rest;
        sum = 0;
        if (!cpf ||
            cpf.length != 11 ||
            cpf == "00000000000" ||
            cpf == "11111111111" ||
            cpf == "22222222222" ||
            cpf == "33333333333" ||
            cpf == "44444444444" ||
            cpf == "55555555555" ||
            cpf == "66666666666" ||
            cpf == "77777777777" ||
            cpf == "88888888888" ||
            cpf == "99999999999") {
            formContext.getAttribute("dcp_cpf").setValue(null);
            return false
        }

        for (i = 1; i <= 9; i++) {
            sum = sum + parseInt(cpf.substring(i - 1, i)) * (11 - i);
        }

        rest = sum % 11;

        if (rest == 10 || rest == 11 || rest < 2) {
            rest = 0;
        } else {
            rest = 11 - rest;
        }

        if (rest != parseInt(cpf.substring(9, 10))) {
            formContext.getAttribute("dcp_cpf").setValue(null);
            return false;
        }

        sum = 0;

        for (i = 1; i <= 10; i++) {
            sum = sum + parseInt(cpf.substring(i - 1, i)) * (12 - i);
        }
        rest = sum % 11;

        if (rest == 10 || rest == 11 || rest < 2) {
            rest = 0;
        } else {
            rest = 11 - rest;
        }

        if (rest != parseInt(cpf.substring(10, 11))) {
            formContext.getAttribute("dcp_cpf").setValue(null);
            return false;
        }

        return true;
    },
}
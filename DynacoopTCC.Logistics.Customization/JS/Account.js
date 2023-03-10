if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account =
{
    OnChangeAccountName: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var name = formContext.getAttribute("name").getValue();
        formContext.getAttribute("name").setValue(this.FirstUppercaseLetter(name));
    },

    OnChangeCNPJ: function (executionContext) {

        var formContext = executionContext.getFormContext();

        var cnpj = formContext.getAttribute("dcp_cnpj").getValue();

        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999" ||
            cnpj == '' ||
            cnpj.length != 14) {

            formContext.getAttribute("dcp_cnpj").setValue("");
            return;
        }

        length = cnpj.length - 2
        numbers = cnpj.substring(0, length);
        digits = cnpj.substring(length);
        plus = 0;
        pos = length - 7;

        for (i = length; i >= 1; i--) {
            plus += numbers.charAt(length - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        result = plus % 11 < 2 ? 0 : 11 - plus % 11;

        if (result != digits.charAt(0)) {

            formContext.getAttribute("dcp_cnpj").setValue("");
            return;
        }

        length = length + 1;
        numbers = cnpj.substring(0, length);
        plus = 0;
        pos = length - 7;

        for (i = length; i >= 1; i--) {
            plus += numbers.charAt(length - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        result = plus % 11 < 2 ? 0 : 11 - plus % 11;

        if (result != digits.charAt(1)) {

            formContext.getAttribute("dcp_cnpj").setValue("");
            return;
        }

        cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
        formContext.getAttribute("dcp_cnpj").setValue(cnpj);
    },

    FirstUppercaseLetter: function (text) {
        var myPhrase = text;

        if (myPhrase != null) {

            var words = myPhrase.split(" ");

            for (let i = 0; i < words.length; i++) {
                if (words[i] != "") {
                    words[i] = words[i][0].toUpperCase() + words[i].substr(1);
                }
            }

            return words.join(" ");
        }

        return myPhrase;
    }
}
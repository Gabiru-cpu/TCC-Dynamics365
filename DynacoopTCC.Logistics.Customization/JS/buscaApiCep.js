if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account = {
	OnCepChange: function (executionContext) {
		var formContext = executionContext.getFormContext();

		var CEP = formContext.getAttribute("address1_postalcode").getValue();

		var execute_dcp_BuscaCEPnaAPI_Request = {

			cep: CEP,

			getMetadata: function () {
				return {
					boundParameter: null,
					parameterTypes: {
						cep: { typeName: "Edm.String", structuralProperty: 1 }
					},
					operationType: 0, operationName: "dcp_BuscaCEPnaAPI"
				};			
			}
		};

		Xrm.WebApi.execute(execute_dcp_BuscaCEPnaAPI_Request).then(
			function success(response) {
				if (response.ok) { return response.json(); }
			}
		).then(function (responseBody) {
			var result = responseBody;
			console.log(result);
			debugger;

			var logradouro = result["logradouro"]; 
			var complemento = result["complemento"]; 
			var bairro = result["bairro"]; 
			var localidade = result["localidade"]; 
			var uf = result["uf"]; 
			var ibge = result["ibge"]; 
			var ddd = result["ddd"]; 
			formContext.getAttribute("address1_line1").setValue(logradouro);
			formContext.getAttribute("address1_line3").setValue(complemento);
			formContext.getAttribute("dcp_bairro").setValue(bairro);
			formContext.getAttribute("address1_stateorprovince").setValue(localidade);
			formContext.getAttribute("dcp_uf").setValue(uf);
			formContext.getAttribute("dcp_ibge1").setValue(ibge);
			formContext.getAttribute("dcp_ddd1").setValue(ddd);
		}).catch(function (error) {
			console.log(error.message);
			debugger;
		});

	}
}
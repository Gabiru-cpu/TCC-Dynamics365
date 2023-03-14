if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Opportunity) == "undefined") { Logistics.Opportunity = {} }

Logistics.Opportunity = {
    Ribbon: {
        var sourceOpportunityId = "{source_opportunity_guid}";
        var targetOpportunityId = "{target_opportunity_guid}";

        var sourceOpportunity = Xrm.WebApi.retrieveRecord("opportunity", sourceOpportunityId);

        var targetOpportunity = {
            "name": sourceOpportunity.name,
            "description": sourceOpportunity.description,
            "estimatedvalue": sourceOpportunity.estimatedvalue,
        };

        Xrm.WebApi.createRecord("opportunity", targetOpportunity).then(
            function success(result) {
                targetOpportunityId = result.id;
                console.log("Target opportunity created with ID: " + targetOpportunityId);
            },
            function (error) {
                console.log(error.message);
            }
        );
    },

    OnLoad: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var integracao = formContext.getAttribute("dcp2_env1opp").getValue();
        if (integracao) {
            formContext.ui.setFormNotification("Oportunidade copiada do ambiente 1: Não pode ser editada.", "INFO", "OpportunityAlert");
            formContext.data.entity.attributes.forEach(
                function (attribute) {
                    var attributeName = attribute.getName();
                    formContext.getControl(attributeName).setDisabled(true);
                }
            );
        }
    }
}
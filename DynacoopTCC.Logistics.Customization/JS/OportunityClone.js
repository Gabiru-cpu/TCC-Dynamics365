if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account = {
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
    }
}
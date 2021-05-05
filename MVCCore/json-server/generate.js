module.exports = function () {
    let faker = require("faker");
    let _ = require("lodash");

    return {
        users: _.times(100, function(n){
            return{
                id: n,
                email: faker.internet.email(),
                first_name: faker.name.firstName(),
                last_name: faker.name.lastName(),
                avatar: faker.internet.avatar()
            }
        })
    }
}
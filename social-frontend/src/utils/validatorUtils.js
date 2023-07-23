import methods from 'validator';
export const validator = (rules, obj) => {
    let errors = {},
        isValid = true;
    rules.forEach((rule) => {
        if(!rule) return;
        if (errors[rule.field]) return;

        const fieldValue = obj[rule.field] || '';
        const args = rule.args || [];

        const validationMethod = typeof rule.method === 'string' ? methods[rule.method] : rule.method;

        if (validationMethod(fieldValue, ...args) !== rule.validWhen) {
            errors[rule.field] = rule.message;
            isValid = false;
        }
    });
    return errors;
};

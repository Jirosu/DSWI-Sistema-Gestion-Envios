const validator = new JustValidate('#formularioLogin');

validator
    .addField('#username', [
        {
            rule: 'required',
            errorMessage: 'Ingrese su usuario',
        },
    ])
    .addField('#password', [
        {
            rule: 'required',
            errorMessage: 'Ingrese su contraseña',
        },
    ]);

validator.onSuccess((event) => {
    event.currentTarget.submit();
})
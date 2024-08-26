const validator = new JustValidate('#formularioEnvio');

validator
    .addField('#paq-cate', [
        {
            rule: 'required',
            errorMessage: 'Seleccione la categoría',
        },
    ])
    .addField('#peso-paq', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el peso del paquete',
        },
        {
            rule: 'number',
        },
        {
            validator: (value) => {
                return (value > 0);
            },
            errorMessage: 'El peso del paquete debe ser mayor a cero',
        }
    ]).addField('#direccion-destino', [
        {
            rule: 'required',
            errorMessage: 'Ingrese la dirección',
        },
    ]).addField('#distrito-dest', [
        {
            rule: 'required',
            errorMessage: 'Seleccione el distrito de destino',
        },
    ]).addField('#nom-remitente', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el nombre del remitente',
        },
    ]).addField('#dni-remitente', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el DNI del remitente',
        },
        {
            rule: 'number',
        },
        {
            validator: (value) => {
                return value.length == 8;
            },
            errorMessage: 'El DNI debe contener 8 caracteres',
        },
    ]).addField('#telf-remitente', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el teléfono del remitente',
        },
        {
            rule: 'number',
        },
        {
            validator: (value) => {
                return (value.length === 7 || value.length === 9);
            },
            errorMessage: 'El teléfono debe contener entre 7 o 9 números',
        },
    ]).addField('#nom-destinatario', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el nombre del destinatario',
        },
    ]).addField('#dni-destinatario', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el DNI del destinatario',
        },
        {
            rule: 'number',
        },
        {
            validator: (value) => {
                return value.length == 8;
            },
            errorMessage: 'El DNI debe contener 8 caracteres',
        },
    ]).addField('#telf-destinatario', [
        {
            rule: 'required',
            errorMessage: 'Ingrese el teléfono del destinatario',
        },
        {
            rule: 'number',
        },
        {
            validator: (value) => {
                return (value.length === 7 || value.length === 9);
            },
            errorMessage: 'El teléfono debe contener entre 7 o 9 números',
        },
    ]);

validator.onSuccess((event) => {
    event.currentTarget.submit();
});


/*----------------------------------------*/
//Calcular costo de envio automaticamente
const txtPeso = document.getElementById('peso-paq');
const cmbDistrito = document.getElementById('distrito-dest');
const txtPrecio_x_kg = document.getElementById('precio_x_KG');
const txtCostoHidden = document.getElementById('costo_envio');
const txtPrecioEnvio = document.getElementById('precio-Envio');


async function obtenerDistritos() {
    const response = await fetch('/Home/listarDistritos');
    const data = await response.json();

    console.log(data);
    return data;
}

async function retornarTarifa(idDistrito) {
    const distritos = await obtenerDistritos();   
    const tarifa = distritos.find(dis => dis.id_distrito === parseInt(idDistrito));

    return tarifa ? tarifa.refTarifa.precio_x_KG : 0
}


const calcularCosto = (tarifa) => {    
    const peso = txtPeso.value;    
    console.log('peso', peso);
    console.log('peso * tarifa', peso * tarifa);

    return peso * tarifa;
}

const mostrarTarifaCosto = async () => {
    const tarifa = await retornarTarifa(cmbDistrito.value) 
    const costo = calcularCosto(tarifa);
    console.log('tarifa',tarifa);
    console.log('costo', costo);

    txtPrecio_x_kg.value = tarifa;
    txtPrecioEnvio.innerHTML = costo.toFixed(2);
    txtCostoHidden.value = costo;
}


txtPeso.addEventListener('change', mostrarTarifaCosto);
cmbDistrito.addEventListener('change', mostrarTarifaCosto);


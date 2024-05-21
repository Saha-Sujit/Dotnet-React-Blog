const loginFormFields = [
  {
    id: 0,
    label: "Username",
    name: "email",
    type: "email",
    placeholder: "Write your email here",
  },
  {
    id: 1,
    label: "Password",
    name: "password",
    type: "password",
    placeholder: "Write your password here",
  },
];

const registerFormFields = [
  {
    id: 0,
    label: "Name",
    name: "name",
    type: "name",
    placeholder: "Write your name here",
  },
  ...loginFormFields,
];

export { loginFormFields, registerFormFields };

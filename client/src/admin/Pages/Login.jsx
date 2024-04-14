import { useForm } from "react-hook-form";
import React from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import authSchema from "../../validationSchemas/authSchema";
import CommonForm from "../../commonComponents/CommonForm";
import authFormFields from "../../formFields/authFormFields";

const Login = () => {
  const form = useForm({ resolver: zodResolver(authSchema) });

  const onSubmit = (data) => console.log("form", data);

  return (
    <>
      <CommonForm
        form={form}
        onSubmit={onSubmit}
        formFields={authFormFields}
        buttonText="Login"
      />
    </>
  );
};

export default Login;

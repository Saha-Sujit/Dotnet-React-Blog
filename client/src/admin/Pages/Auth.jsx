import { useForm } from "react-hook-form";
import React from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  loginSchema,
  registerSchema,
} from "../../validationSchemas/authSchema";
import CommonForm from "../../commonComponents/CommonForm";
import {
  loginFormFields,
  registerFormFields,
} from "../../formFields/authFormFields";
import { QueryClient, useMutation } from "@tanstack/react-query";
import axios from "axios";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Link, useHref } from "react-router-dom";

const Auth = () => {
  const hrefLocation = useHref();
  const isLogin = hrefLocation === "/auth/login";
  const authEndpoint = isLogin ? "Login" : "Register";
  const authRoutes = isLogin ? "Register" : "Login";
  const authSchema = isLogin ? loginSchema : registerSchema;
  const authFormFields = isLogin ? loginFormFields : registerFormFields;
  const form = useForm({ resolver: zodResolver(authSchema) });

  const mutation = useMutation({
    mutationFn: (data) => {
      axios
        .post(`${process.env.REACT_APP_BASE_URL}/User/${authEndpoint}`, data)
        .then((res) => console.log(res))
        .catch((error) => console.log(error));
    },
    onSuccess: () => {
      QueryClient.invalidateQueries({ queryKey: ["login"] });
    },
  });

  const onSubmit = (data) => {
    mutation.mutate(data);
    console.log("form", data);
  };

  return (
    <div className="flex justify-center items-center h-screen bg-slate-900">
      <Card className="w-1/4 bg-slate-800 text-white border-slate-800">
        <CardHeader>
          <CardTitle>{authEndpoint}</CardTitle>
          <CardDescription>
            {`Enter your ${isLogin ? "" : "Name,"} Username and Password to ${
              isLogin ? "Login" : "Register"
            }.`}
          </CardDescription>
        </CardHeader>
        <CardContent>
          <CommonForm
            className="bg-inherit border-inherit"
            form={form}
            onSubmit={onSubmit}
            formFields={authFormFields}
            buttonText={authEndpoint}
          />
        </CardContent>
        <CardFooter>
          <p>
            {`${isLogin ? "Not" : "Already"} Have an Account, `}
            <Link to={`/auth/${authRoutes.toLowerCase()}`}>{authRoutes}</Link>
          </p>
        </CardFooter>
      </Card>
    </div>
  );
};

export default Auth;

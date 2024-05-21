import React from "react";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../components/ui/form";
import { Input } from "../components/ui/input";
import { Button } from "../components/ui/button";

const CommonForm = ({ form, onSubmit, formFields, buttonText, className }) => {
  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        {formFields.map(({ id, label, name, type, placeholder }) => (
          <FormField
            key={id}
            control={form.control}
            name={name}
            render={({ field }) => (
              <FormItem className="mt-2">
                <FormLabel>{label}</FormLabel>
                <FormControl>
                  <Input
                    type={type}
                    placeholder={placeholder}
                    {...field}
                    className={className}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        ))}
        <Button type="submit" className="w-full mt-10">
          {buttonText}
        </Button>
      </form>
    </Form>
  );
};

export default CommonForm;

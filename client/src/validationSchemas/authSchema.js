import { z } from "zod";

const loginSchema = z.object({
  email: z
    .string()
    .min(2, {
      message: "Username must be at least 2 characters.",
    })
    .email({
      message: "Please enter a valid email address.",
      minLength: 1,
    }),
  password: z.string().min(8, {
    message: "Password must be at least 8 characters.",
  }),
});

const registerSchema = loginSchema
  .pick({ email: true, password: true }) // Pick email and password fields
  .merge(
    z.object({
      name: z.string().min(2, {
        message: "Name must be at least 2 characters.",
      }),
    })
  );

export { loginSchema, registerSchema };

import { z } from "zod";

const authSchema = z.object({
  username: z
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

export default authSchema;

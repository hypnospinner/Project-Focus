namespace ProjectFocus.Backend.Service.Identity

module Domain =

    type NewUserParams =
        {
            Name: string;
            Email: string;
            Password: string;
        }

    type LoginUserParams =
        {
            Email: string;
            Password: string;
        }

    type EncryptionParams =
         {
             Password: string;
             Salt: byte[];
         }

import { Container, TextField, Button, Box } from "@mui/material";
import { FC, useState } from "react";
import { INewUser } from "../../interfaces/users";
import * as userApi from "../../api/modules/users"

const CreateUser: FC = () => {
    const [name, setName] = useState('');
    const [job, setJob] = useState('');
    const [error, setError] = useState({ name: false, job: false });

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (!name || !job) {
            setError({ name: !name, job: !job });
            return;
        }
        const user: INewUser = {
            name,
            job,
        };
        try {
            await userApi.create(user);
            console.log('User created');
            setName(''); 
            setJob(''); 
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <Container>
            <Box
                component="form"
                sx={{
                    '& .MuiTextField-root': { m: 1, width: '25ch' },
                }}
                noValidate
                autoComplete="off"
                onSubmit={handleSubmit}
            >
                <TextField
                    error={error.name}
                    helperText={error.name ? "Name is required" : ""}
                    label="Name"
                    value={name}
                    onChange={(e) => {
                        setName(e.target.value);
                        setError((prevError) => ({ ...prevError, name: false }));
                    }}
                    required
                    sx={{ mb: 2 }}
                />
                <TextField
                    error={error.job}
                    helperText={error.job ? "Job is required" : ""}
                    label="Job"
                    value={job}
                    onChange={(e) => {
                        setJob(e.target.value);
                        setError((prevError) => ({ ...prevError, job: false }));
                    }}
                    required
                    sx={{ mb: 2 }}
                />
                <Button 
                    type="submit" 
                    variant="contained" 
                    color="success"
                    sx={{ 
                        m: 1, 
                        width: '25ch', 
                        pt: '6px',
                        pb: '6px',
                        fontSize: '25px'
                    }}
                >
                    CREATE
                </Button>
            </Box>
        </Container>
    );
};

export default CreateUser;
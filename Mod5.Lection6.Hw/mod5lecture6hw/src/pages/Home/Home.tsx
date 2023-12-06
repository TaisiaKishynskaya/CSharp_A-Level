import React, {ReactElement, FC, useEffect, useState} from "react";
import {Box, Button, CircularProgress, Container, Grid, Pagination} from '@mui/material'
import * as userApi from "../../api/modules/users"
import {IUser} from "../../interfaces/users";
import UserCard from "./components/UserCard";
import { useNavigate } from 'react-router-dom';

const Home: FC<any> = (): ReactElement => {
    const [users, setUsers] = useState<IUser[] | null>(null)
    const [totalPages, setTotalPages] = useState<number>(0)
    const [currentPage, setCurrentPage] = useState<number>(1)
    const [isLoading, setIsLoading] = useState<boolean>(false)
    const navigate = useNavigate();

    useEffect(() => {
        const getUser = async () => {
            try {
                setIsLoading(true)
                const res = await userApi.getByPage(currentPage)
                setUsers(res.data)
                setTotalPages(res.total_pages)
            } catch (e) {
                if (e instanceof Error) {
                    console.error(e.message)
                }
            }
            setIsLoading(false)
        }
        getUser()
    }, [currentPage])

  return (
      <Container>
          <Grid container spacing={4} justifyContent="center" my={4}>
              {isLoading ? (
                  <CircularProgress />
              ) : (
                  <>
                      {users?.map((item) => (
                          <Grid key={item.id} item lg={2} md={4} xs={6}>
                              <UserCard {...item} />
                          </Grid>
                      ))}
                  </>
              )}
          </Grid>
          <Box
              sx={{
                  display: 'flex',
                  justifyContent: 'center'
              }}
          >
              <Pagination count={totalPages} page={currentPage} onChange={ (event, page)=> setCurrentPage(page)} />
          </Box>
          <Button 
            onClick={() => navigate('/create-user')} 
            variant="contained" 
            color="primary"
            sx={{ 
                mt: 2,
                mb: 2,
                width: '100%',
                height: '50px',
                fontSize: '20px',
                fontWeight: 'bold',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}
            size="large"
        >
            Create User
        </Button>
      </Container>
  );
};

export default Home;
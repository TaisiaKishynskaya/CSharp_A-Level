import {Card, CardActionArea, CardContent, Typography} from "@mui/material"
import {FC, ReactElement} from "react";
import {IResource} from "../../../interfaces/resources";
import {useNavigate} from "react-router-dom";

const ResourceCard: FC<IResource> = (props): ReactElement => {

    const navigate = useNavigate()

     return (
        <Card sx={{ maxWidth: 250, backgroundColor: props.color }}> 
            <CardActionArea onClick={() => navigate(`/resource/${props.id}`)}>
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component="div">
                        {props.name}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Year: {props.year}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Color: {props.color}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Pantone Value: {props.pantone_value}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    )
}

export default ResourceCard
import { Card } from "../../components/ui/card";
import { Button } from "../../components/ui/button";
import React from "react";
import { Link } from "react-router-dom";

const Posts = () => {
  return (
    <Card className="mx-2">
      <div className="flex justify-between m-5">
        <h1 className="text-xl font-medium">Posts</h1>
        <Button>
          <Link to="/">Add Post</Link>
        </Button>
      </div>
    </Card>
  );
};

export default Posts;

SELECT p.product_id as 'p_id', c.category_id as 'c_id', pd.name as 'p_name', p.price as 'p_price', p.quantity as 'p_quantity', p.image as 'p_image', cd.name as 'c_name'  FROM product as p 
left join product_description as pd on pd.product_id = p.product_id
left join product_to_category as pc on pc.product_id = p.product_id
left join category as c on c.category_id = pc.category_id
left join category_description as cd on cd.category_id = c.category_id
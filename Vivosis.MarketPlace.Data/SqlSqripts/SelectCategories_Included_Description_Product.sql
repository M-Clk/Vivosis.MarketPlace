SELECT * FROM ofsimo_opencart.category as c 
left join category_description as d on d.category_id = c.category_id
left join product_to_category as pc on pc.category_id = c.category_id
left join product as p on p.product_id = pc.product_id
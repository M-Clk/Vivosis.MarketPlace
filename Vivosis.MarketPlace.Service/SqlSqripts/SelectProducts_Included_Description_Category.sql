SELECT * FROM ofsimo_opencart.product as p 
left join product_description as d on d.product_id = p.product_id
left join product_to_category as pc on pc.product_id = p.product_id
left join category as c on c.category_id = pc.category_id
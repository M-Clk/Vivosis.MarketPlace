SELECT 
po.product_option_id as 'product_option_id',
po.required as 'product_option_required',
po.value as 'product_option_value',
po.product_id as 'product_option_product_id',
po.option_id as 'product_option_option_id',
pov.product_option_value_id as 'product_option_value_id',
pov.option_value_id as 'option_value_id',
pov.quantity as 'product_option_value_quantity',
CONCAT(pov.price_prefix, pov.price) as 'product_option_value_price',
CONCAT(pov.points_prefix, pov.points) as 'product_option_value_points',
CONCAT(pov.weight_prefix, pov.weight) as 'product_option_value_weight'
FROM product_option as po
left join product_option_value as pov on pov.product_option_id = po.product_option_id
-- Script para crear el primer usuario administrador
-- Este script debe ejecutarse DESPUÉS de aplicar las migraciones

-- El password es: Admin123!
-- Hash generado con BCrypt work factor 12

INSERT INTO usuarios (identificador, nombre_usuario, pass_hash, fecha_creacion)
VALUES (
  gen_random_uuid(),
  'admin',
  '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5GyYIgaT6Z6Wm',
  NOW()
)
ON CONFLICT (nombre_usuario) DO NOTHING;

-- Verificar que se creó
SELECT identificador, nombre_usuario, fecha_creacion 
FROM usuarios 
WHERE nombre_usuario = 'admin';
